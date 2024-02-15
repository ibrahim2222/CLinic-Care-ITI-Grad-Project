using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.EmailServices;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Clinc_Care_MVC_Grad_PROJ.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    public class AppointmentController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }
        public IEmailSender EmailSender { get; }

        public AppointmentController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            UnitOfWork = unitOfWork;
            EmailSender = emailSender;
        }

        private MemoryStream DownloadSinghFile(string filename, string uploadPath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, filename);
            var memory = new MemoryStream();
            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new MemoryStream(data);
                memory = content;
            }
            memory.Position = 0;
            return memory;
        }

        [AllowAnonymous]
        public ActionResult DownloadFile(string fileName, string type)
        {
            var path = $"wwwroot\\{type}";
            var memory = DownloadSinghFile(fileName, path);
            return File(memory.ToArray(), "application/pdf", "Results.pdf");
        }
        public ActionResult Index()
        {
            return View("Index", UnitOfWork.AppointmentRepository.GetAllAppointments());
        }
        public ActionResult Details(int id)
        {
            return View(UnitOfWork.AppointmentRepository.GetAppointmentById(id));
        }
        [AllowAnonymous]
        public ActionResult GetLabOrders(int id)
        {
            return PartialView("_LapOrderDetails", UnitOfWork.AppointmentRepository.GetAppointmentById(id));
        }
        [AllowAnonymous]
        public ActionResult GetRadiologyOrders(int id)
        {
            return PartialView("_RadiologyOrderDetails", UnitOfWork.AppointmentRepository.GetAppointmentById(id));
        }
        [AllowAnonymous]
        public ActionResult GetMedicinesOrders(int id)
        {
            return PartialView("_MedicineOrderDetails", UnitOfWork.AppointmentRepository.GetAppointmentById(id));
        }
        public ActionResult GetAppointmentDetails(int id)
        {
            return PartialView("_AppointmentDetailsModal", UnitOfWork.AppointmentRepository.GetAppointmentById(id));
        }

        public ActionResult ConfirmAppointmentsDetails(int id)
        {
            return PartialView("_TodayAppointmentModel", UnitOfWork.AppointmentRepository.GetAppointmentById(id));
        }

        public ActionResult Prescription(int id)
        {
            return View("prescription", UnitOfWork.AppointmentRepository.GetAppointmentById(id));
        }
        public ActionResult Create()
        {
            ViewBag.AppointmentTypelist = new SelectList(UnitOfWork.AppointmentTypeRepository.GetAllAppointmentTypes(), "AppointmentTypeId", "TypeName");
            ViewBag.PaymentMathodlist = new SelectList(UnitOfWork.PaymentMethodRepository.GetAllPaymentMethods(), "PaymentMethodId", "PaymentMethodName");
            ViewBag.Patientlist = new SelectList(UnitOfWork.PatientRepository.GetAllPatients(), "PatientId", "PatientFirstName");
            ViewBag.EmpDoctorSpecilzationlist = new SelectList(UnitOfWork.EmployeeRepository.GetAllEmployees().Where(e=>e.EmployeeTypeIdNavigation.TypeName == "Doctor"), "EmpId", "EmployeeFullName");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Appointment appointment)
        {
            ViewBag.AppointmentTypelist = new SelectList(UnitOfWork.AppointmentTypeRepository.GetAllAppointmentTypes(), "AppointmentTypeId", "TypeName");
            ViewBag.PaymentMathodlist = new SelectList(UnitOfWork.PaymentMethodRepository.GetAllPaymentMethods(), "PaymentMethodId", "PaymentMethodName");
            ViewBag.Patientlist = new SelectList(UnitOfWork.PatientRepository.GetAllPatients(), "PatientId", "PatientFirstName");
            ViewBag.EmpDoctorSpecilzationlist = new SelectList(UnitOfWork.EmpDoctorSpecializionRepository.GetAllEmpDoctorSpecilzations(), "EmpDoctorSpecilzationId", "EmpDoctorSpecilzationId");

            if (ModelState.IsValid)
            {
                UnitOfWork.AppointmentRepository.InsertAppointment(appointment);
                return RedirectToAction("TodayAppointment", "HomeDashboard");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.AppointmentTypelist = new SelectList(UnitOfWork.AppointmentTypeRepository.GetAllAppointmentTypes(), "AppointmentTypeId", "TypeName");
            ViewBag.PaymentMathodlist = new SelectList(UnitOfWork.PaymentMethodRepository.GetAllPaymentMethods(), "PaymentMethodId", "PaymentMethodName");
            ViewBag.Patientlist = new SelectList(UnitOfWork.PatientRepository.GetAllPatients(), "PatientId", "PatientFirstName");
            ViewBag.EmpDoctorSpecilzationlist = new SelectList(UnitOfWork.EmpDoctorSpecializionRepository.GetAllEmpDoctorSpecilzations(), "EmpDoctorSpecilzationId", "EmpDoctorSpecilzationId");

            return View(UnitOfWork.AppointmentRepository.GetAppointmentById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Appointment appointment)
        {
            ViewBag.AppointmentTypelist = new SelectList(UnitOfWork.AppointmentTypeRepository.GetAllAppointmentTypes(), "AppointmentTypeId", "TypeName");
            ViewBag.PaymentMathodlist = new SelectList(UnitOfWork.PaymentMethodRepository.GetAllPaymentMethods(), "PaymentMethodId", "PaymentMethodName");
            ViewBag.Patientlist = new SelectList(UnitOfWork.PatientRepository.GetAllPatients(), "PatientId", "PatientFirstName");
            ViewBag.EmpDoctorSpecilzationlist = new SelectList(UnitOfWork.EmpDoctorSpecializionRepository.GetAllEmpDoctorSpecilzations(), "EmpDoctorSpecilzationId", "EmpDoctorSpecilzationId");

            if (ModelState.IsValid)
            {
                UnitOfWork.AppointmentRepository.UpdateAppointment(id, appointment);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<ActionResult> Delete(int id)
        {
            UnitOfWork.AppointmentRepository.DeleteAppointment(id, false);
            var MyApp = UnitOfWork.AppointmentRepository.GetAppointmentById(id);

            var fees = UnitOfWork.FeeRepository.GetAllFees().Where(f => f.FeeName == "Booking").FirstOrDefault().FeeAmount;
            var SpciName = MyApp.DoctorIdNavigation.EmpDoctorSpecilzations.FirstOrDefault().DoctorSpecilzationIdNavigation.SpecializationName;
            var DrName = UnitOfWork.EmpDoctorSpecializionRepository.GetAllEmpDoctorSpecilzations().Where(ds => ds.DoctorSpecilzationIdNavigation.SpecializationName == SpciName).FirstOrDefault().DoctorIdNavigation.EmployeeFullName;
            var AppDate = MyApp.AppointmentDate.ToString("yyyy-MM-dd");
            if (MyApp.IsCanceled == true)
            {
                var message = new Message(new string[] { User.Identity.Name }, "Clinic Appointment Cancellation Mail", $"<h3>Hello,Your Appointment Just cancelled For {SpciName} with Dr/ {DrName} on {AppDate}</h3></br><p>Total Fees: {fees}</p>", null);
                await EmailSender.SendEmailAsync(message);
            }
            else
            {
                var message = new Message(new string[] { User.Identity.Name }, "Clinic Appointment Recovering Mail", $"<h3>Hello,Your Appointment Just Recoverd For {SpciName} with Dr/ {DrName} on {AppDate}</h3></br><p>Total Fees: {fees}</p>", null);
                await EmailSender.SendEmailAsync(message);
            }
            return RedirectToAction("AllAppointment", "HomeDashboard", new { area = "Managment" });
        }
        [AllowAnonymous]
        public async Task<ActionResult> PatientProfileDelete(int id)
        {
            UnitOfWork.AppointmentRepository.DeleteAppointment(id, false);
            var MyApp = UnitOfWork.AppointmentRepository.GetAppointmentById(id);

            var fees = UnitOfWork.FeeRepository.GetAllFees().Where(f => f.FeeName == "Booking").FirstOrDefault().FeeAmount;
            var SpciName = MyApp.DoctorIdNavigation.EmpDoctorSpecilzations.FirstOrDefault().DoctorSpecilzationIdNavigation.SpecializationName;
            var DrName = UnitOfWork.EmpDoctorSpecializionRepository.GetAllEmpDoctorSpecilzations().Where(ds => ds.DoctorSpecilzationIdNavigation.SpecializationName == SpciName).FirstOrDefault().DoctorIdNavigation.EmployeeFullName;
            var AppDate = MyApp.AppointmentDate.ToString("yyyy-MM-dd");
            if (MyApp.IsCanceled == true)
            {
                var message = new Message(new string[] { User.Identity.Name }, "Clinic Appointment Cancellation Mail", $"<h3>Hello,Your Appointment Just cancelled For {SpciName} with Dr/ {DrName} on {AppDate}</h3></br><p>Total Fees: {fees}</p>", null);
                await EmailSender.SendEmailAsync(message);
            }
            else
            {
                var message = new Message(new string[] { User.Identity.Name }, "Clinic Appointment Recovering Mail", $"<h3>Hello,Your Appointment Just Recoverd For {SpciName} with Dr/ {DrName} on {AppDate}</h3></br><p>Total Fees: {fees}</p>", null);
                await EmailSender.SendEmailAsync(message);
            }
            return RedirectToAction("Profile", "Home", new { area = "" });
        }

        public ActionResult DoctorReadyAppointments()
        {
            return View("DoctorReadyAppointments", UnitOfWork.AppointmentRepository.GetAllAppointments());
        }
        public ActionResult ReadyAppointmentDetails(int id)
        {
            return View("ReadyAppointmentDetails", UnitOfWork.AppointmentRepository.GetAppointmentById(id));
        }
        public ActionResult TodayAppointment(int id)
        {
            return View();
        }

        public ActionResult AppointmentPatientDiagnosis(AppPatientDiagnosisVM appPatientDiagnosisVM)
        {
            var routeValues = new RouteValueDictionary(new { id = appPatientDiagnosisVM.AppointmentId, area = "Managment" });
            var Appointment = UnitOfWork.AppointmentRepository.GetAppointmentById(appPatientDiagnosisVM.AppointmentId);
            if (Appointment != null)
            {

                if (appPatientDiagnosisVM.Diagnosis != null)
                {
                    Appointment.ExaminationComment = appPatientDiagnosisVM.Diagnosis;
                    UnitOfWork.AppointmentRepository.UpdateAppointment(Appointment.AppointmentId, Appointment);
                }
            }

            return RedirectToAction("DetailedPatientAppointment", "Employee", routeValues);


        }
        public ActionResult MarkAsTreated(int id)
        {
            var routeValues = new RouteValueDictionary(new { id = id    , area = "Managment" });

            var Appointment = UnitOfWork.AppointmentRepository.GetAppointmentById(id);
            if (Appointment != null)
            {

                Appointment.AppointmentState = AppointmentStates.treated;
                UnitOfWork.AppointmentRepository.UpdateAppointment(id, Appointment);

            }
            return RedirectToAction("DoctorReadyAppoitments", "Employee", routeValues);

        }
        public ActionResult Update(int id)
        {

            var App = UnitOfWork.AppointmentRepository.GetAppointmentById(id);
            if (App != null)
            {
                App.AppointmentState = AppointmentStates.queued;
                if(App.IsPaid == false)
                {
                    App.IsPaid = true;
                }
                UnitOfWork.AppointmentRepository.UpdateAppointment(id, App);

            }
            var routeValues = new RouteValueDictionary(new { area = "Managment" });
            return RedirectToAction("TodayAppointment", "HomeDashboard", routeValues);
        }



    }
}
