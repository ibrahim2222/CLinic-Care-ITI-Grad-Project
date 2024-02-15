using Microsoft.AspNetCore.Authorization;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Clinc_Care_MVC_Grad_PROJ.Models.ViewModels;

namespace Resturant_RES_MVC_ITI_PRJ.Areas.Client.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Managment")]
    public class MedicineOrderController : Controller
    {

        public IUnitOfWork UnitOfWork { get; }

        public MedicineOrderController(IUnitOfWork unitOfWork)
        {

            UnitOfWork = unitOfWork;
        }


        public ActionResult Index()
        {

            return View("Index", UnitOfWork.MedicineOrderRepository.GetAllMedicineOrders());
        }


        public ActionResult Details(int id)
        {
            return View("Details", UnitOfWork.MedicineOrderRepository.GetMedicineOrderById(id));
        }

        public ActionResult Create()
        {
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId", "AppointmentId");
            ViewBag.Medicinelst = new SelectList(UnitOfWork.MedicineRepository.GetAllMedicines(), "MedicineId", "MedicineName");
            ViewBag.Dosagelst = new SelectList(UnitOfWork.DosageRepository.GetAllDosages(), "DosageId", "DosageName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicineOrder medicineOrder)
        {
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId", "AppointmentId");
            ViewBag.Medicinelst = new SelectList(UnitOfWork.MedicineRepository.GetAllMedicines(), "MedicineId", "MedicineName");
            ViewBag.Dosagelst = new SelectList(UnitOfWork.DosageRepository.GetAllDosages(), "DosageId", "DosageName");

            if (ModelState.IsValid)
            {
                UnitOfWork.MedicineOrderRepository.InsertMedicineOrder(medicineOrder);
                return RedirectToAction("index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AppointmentPatientMedicineOrders(MedicineOrder medicineOrder)
        {

            var routeValues = new RouteValueDictionary(new { id = medicineOrder.AppointmentId });


            if (medicineOrder != null)
            {

                    UnitOfWork.MedicineOrderRepository.InsertMedicineOrder(medicineOrder);

                

            }
            return RedirectToAction("DetailedPatientAppointment", "Employee", routeValues);

        }

        [HttpPost]
        public ActionResult AppointmentPatientMedicineOrdersEdit(MedicineOrder medicineOrder)
        {
            var ListOfOrders = UnitOfWork.MedicineOrderRepository.GetAllMedicineOrders().Where(lo => lo.AppointmentId == medicineOrder.AppointmentId);

            foreach (var medicine in ListOfOrders)
            {
                UnitOfWork.LaboratoryOrderRepository.DeleteLaboratoryOrder(medicine.MedicineOrderId);
            }

            var routeValues = new RouteValueDictionary(new { id = medicineOrder.AppointmentId });


            if (medicineOrder != null)
            {

                    UnitOfWork.MedicineOrderRepository.InsertMedicineOrder(medicineOrder);

            }
            return RedirectToAction("DetailedPatientAppointment", "Employee", routeValues);

        }

        public ActionResult Edit(int id)
        {
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId", "AppointmentId");
            ViewBag.Medicinelst = new SelectList(UnitOfWork.MedicineRepository.GetAllMedicines(), "MedicineId", "MedicineName");
            ViewBag.Dosagelst = new SelectList(UnitOfWork.DosageRepository.GetAllDosages(), "DosageId", "DosageName");

            return View(UnitOfWork.MedicineOrderRepository.GetMedicineOrderById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MedicineOrder medicineOrder)
        {
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId", "AppointmentId");
            ViewBag.Medicinelst = new SelectList(UnitOfWork.MedicineRepository.GetAllMedicines(), "MedicineId", "MedicineName");
            ViewBag.Dosagelst = new SelectList(UnitOfWork.DosageRepository.GetAllDosages(), "DosageId", "DosageName");

            if (ModelState.IsValid)
            {
                UnitOfWork.MedicineOrderRepository.UpdateMedicineOrder(id, medicineOrder);
                return RedirectToAction("index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            UnitOfWork.MedicineOrderRepository.DeleteMedicineOrder(id);
            return RedirectToAction("index");
        }       
        public ActionResult DoctorDelete(int id)
        {
            var appid = UnitOfWork.MedicineOrderRepository.GetMedicineOrderById(id).AppointmentId;
            var routeValues = new RouteValueDictionary(new { id = appid });

            UnitOfWork.MedicineOrderRepository.DeleteMedicineOrder(id);

            return RedirectToAction("DetailedPatientAppointment", "Employee", routeValues);
        }
    }
}

