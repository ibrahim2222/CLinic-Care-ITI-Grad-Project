using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Security.Claims;
using Org.BouncyCastle.Ocsp;
using Clinc_Care_MVC_Grad_PROJ.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Managment")]
    public class DoctorScheduleController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }
        public DoctorScheduleController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View("Index", UnitOfWork.DoctorScheduleRepository.GetAllDoctorSchedule());
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ScheduleBySpecilzationApi(int id)
        {
            var DrID = UnitOfWork.DoctorSpecializionRepository.GetDoctorSpecializionById(id).EmpDoctorSpecilzations.Select(e => e.DoctorId).FirstOrDefault();
            DaysOfWeek Schdeule = UnitOfWork.DoctorScheduleRepository.GetAllDoctorSchedule().Where(ds => ds.DoctorId == DrID).Select(e => e.DaysOfWeek).FirstOrDefault();
            var CurrentPatient = UnitOfWork.PatientRepository.GetAllPatients().SingleOrDefault(p => p.PatientEmail == User.FindFirst(ClaimTypes.Name)?.Value).PatientId;
            var ApposDates = UnitOfWork.AppointmentRepository
                .GetAllAppointments()
                .Where(a => a.PatientId == CurrentPatient && a.DoctorIdNavigation.EmpDoctorSpecilzations.FirstOrDefault().DoctorSpecilzationIdNavigation.SpecializationName == UnitOfWork.DoctorSpecializionRepository.GetDoctorSpecializionById(id).SpecializationName)
                .Select(a => new { Date = a.AppointmentDate.ToString("yyyy-MM-dd"), CancelFlag = a.IsCanceled, CreateDate = a.CreatedAppointmentDate });


            var selectedDays = new List<string>();
            var daysOfWeek = Enum.GetValues(typeof(DaysOfWeek)).Cast<DaysOfWeek>();


            foreach (var day in daysOfWeek)
            {
                if ((Schdeule & day) == day && (Schdeule & day) != DaysOfWeek.None)
                {
                    selectedDays.Add(day.ToString());
                }
            }
            var scheduleInfo = new
            {
                selectedDays,
                apposDates = ApposDates
            };
            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
            var serializedSchedule = JsonSerializer.Serialize(scheduleInfo, jsonOptions);

            return new ContentResult
            {
                Content = serializedSchedule,
                ContentType = "application/json"
            };
        }
        public ActionResult Details(int id)
        {
            return View("Details", UnitOfWork.DoctorScheduleRepository.GetDoctorDoctorScheduleById(id));
        }
        public ActionResult Create()
        {
            ViewBag.DoctorList = new SelectList(UnitOfWork.EmployeeRepository
                   .GetAllEmployees()
                   .Where(es => es.EmployeeTypeIdNavigation.TypeName == "Doctor" && es.DoctorSchedules.Count < 1)
                   .Select(e => new { EmpId = e.EmpId, EmployeeFullName = "Dr. " + e.EmployeeFullName + " - " + UnitOfWork.EmpDoctorSpecializionRepository.GetAllEmpDoctorSpecilzations().Where(s => s.DoctorId == e.EmpId).FirstOrDefault().DoctorSpecilzationIdNavigation.SpecializationName })
                   , "EmpId", "EmployeeFullName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SchedVm doctorSchedule)
        {
            ViewBag.DoctorList = new SelectList(UnitOfWork.EmployeeRepository
               .GetAllEmployees()
               .Where(es => es.EmployeeTypeIdNavigation.TypeName == "Doctor" && es.DoctorSchedules.Count < 1)
               .Select(e => new { EmpId = e.EmpId, EmployeeFullName = "Dr. " + e.EmployeeFullName + " - " + UnitOfWork.EmpDoctorSpecializionRepository.GetAllEmpDoctorSpecilzations().Where(s => s.DoctorId == e.EmpId).FirstOrDefault().DoctorSpecilzationIdNavigation.SpecializationName })
               , "EmpId", "EmployeeFullName");
            int selectedDaysSum = 0;

            if (doctorSchedule.DaysOfWeek == null)
            {
                ModelState.AddModelError("All", "Please Choose at least one day");
            }
            else
            {
                selectedDaysSum = doctorSchedule.DaysOfWeek.Sum();
            }

            var DrSch = new DoctorSchedule()
            {
                DoctorId = doctorSchedule.DoctorId,
                DaysOfWeek = (DaysOfWeek)selectedDaysSum
            };


            if (ModelState.IsValid)
            {
                UnitOfWork.DoctorScheduleRepository.InsertDoctorDoctorSchedule(DrSch);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.DoctorList = new SelectList(UnitOfWork.EmployeeRepository
                .GetAllEmployees()
                .Where(es => es.EmployeeTypeIdNavigation.TypeName == "Doctor")
                .Select(e => new { EmpId = e.EmpId, EmployeeFullName = "Dr. " + e.EmployeeFullName + " - " + UnitOfWork.EmpDoctorSpecializionRepository.GetAllEmpDoctorSpecilzations().Where(s => s.DoctorId == e.EmpId).FirstOrDefault().DoctorSpecilzationIdNavigation.SpecializationName })
                , "EmpId", "EmployeeFullName");
            return View(UnitOfWork.DoctorScheduleRepository.GetDoctorDoctorScheduleById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SchedVm doctorSchedule)
        {
            ViewBag.DoctorList = new SelectList(UnitOfWork.EmployeeRepository
               .GetAllEmployees()
               .Where(es => es.EmployeeTypeIdNavigation.TypeName == "Doctor")
               .Select(e => new { EmpId = e.EmpId, EmployeeFullName = "Dr. " + e.EmployeeFullName + " - " + UnitOfWork.EmpDoctorSpecializionRepository.GetAllEmpDoctorSpecilzations().Where(s => s.DoctorId == e.EmpId).FirstOrDefault().DoctorSpecilzationIdNavigation.SpecializationName })
               , "EmpId", "EmployeeFullName");

            int selectedDaysSum = 0;

            if (doctorSchedule.DaysOfWeek == null)
            {
                ModelState.AddModelError("All", "Please Choose at least one day");
            }
            else
            {
                selectedDaysSum = doctorSchedule.DaysOfWeek.Sum();
            }

            if (ModelState.IsValid)
            {
                var DrSch = new DoctorSchedule()
                {
                    DoctorId = doctorSchedule.DoctorId,
                    DaysOfWeek = (DaysOfWeek)selectedDaysSum
                };
                UnitOfWork.DoctorScheduleRepository.UpdateDoctorSchedule(id, DrSch);
                return RedirectToAction(nameof(Index));
            }
            return View(UnitOfWork.DoctorScheduleRepository.GetDoctorDoctorScheduleById(id));
        }
        /*   public ActionResult Delete(int id)
           {
               UnitOfWork.DoctorScheduleRepository.DeleteDoctorSchedule(id);
               return RedirectToAction(nameof(Index));
           }*/
    }
}
