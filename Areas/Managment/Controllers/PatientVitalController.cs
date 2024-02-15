using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Resturant_RES_MVC_ITI_PRJ.Areas.Client.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Managment")]
    public class PatientVitalController : Controller
    {
       
        public IUnitOfWork UnitOfWork { get; }

        public PatientVitalController(IUnitOfWork unitOfWork)
        {
           
            UnitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {

            return View("Index", UnitOfWork.AppointmentRepository.GetAllAppointments());
        }
     

        public ActionResult Details(int id)
        {
            return View("Details", UnitOfWork.PatientVitalRepository.GetPatientVitalById(id));
        }

        public ActionResult Create(int id)
        {
            ViewBag.Nurselst = new SelectList(UnitOfWork.EmployeeRepository.GetAllEmployees().Where(e=>e.EmployeeTypeIdNavigation.TypeName.ToLower()=="nurse"), "EmpId", "EmpFirstName");
            ViewBag.Appointmentlst = UnitOfWork.AppointmentRepository.GetAllAppointments().Where(App=>App.AppointmentId == id).FirstOrDefault().AppointmentId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientVital patientVital)
        {

            ViewBag.Nurselst = new SelectList(UnitOfWork.EmployeeRepository.GetAllEmployees().Where(e => e.EmployeeTypeIdNavigation.TypeName.ToLower() == "nurse"), "EmpId", "EmpFirstName");
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId", "AppointmentId");

            if (ModelState.IsValid)
            {
                UnitOfWork.PatientVitalRepository.InsertPatientVital(patientVital);
                return RedirectToAction("index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Nurselst = new SelectList(UnitOfWork.EmployeeRepository.GetAllEmployees().Where(e => e.EmployeeTypeIdNavigation.TypeName.ToLower() == "nurse"), "EmpId", "EmpFirstName");
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId");

            return View(UnitOfWork.PatientVitalRepository.GetPatientVitalById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PatientVital patientVital)
        {
            ViewBag.Nurselst = new SelectList(UnitOfWork.EmployeeRepository.GetAllEmployees().Where(e => e.EmployeeTypeIdNavigation.TypeName.ToLower() == "nurse"), "EmpId", "EmpFirstName");
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId");

            if (ModelState.IsValid)
            {
                UnitOfWork.PatientVitalRepository.UpdatePatientVital(id, patientVital);
                return RedirectToAction("index");
            }
            return View();
        }

        //public ActionResult Delete(int id)
        //{
        //    UnitOfWork.PatientVitalRepository.DeletePatientVital(id);
        //    return RedirectToAction("index");
        //}
    }
}

