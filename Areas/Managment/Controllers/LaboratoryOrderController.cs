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
    public class LaboratoryOrderController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }

        public LaboratoryOrderController(IUnitOfWork unitOfWork)
        {

            UnitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View("Index", UnitOfWork.LaboratoryOrderRepository.GetAllLaboratoryOrders());
        }
        public ActionResult Details(int id)
        {
            return View("Details", UnitOfWork.LaboratoryOrderRepository.GetLaboratoryOrderById(id));
        }

        public ActionResult Create()
        {
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId", "AppointmentId");
            ViewBag.Labtypelst = new SelectList(UnitOfWork.LabratoryTypeRepository.GetAllLabTypes(), "LabTypeId", "LabName");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LaboratoryOrder laboratoryOrder)
        {
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId", "AppointmentId");
            ViewBag.Labtypelst = new SelectList(UnitOfWork.LabratoryTypeRepository.GetAllLabTypes(), "LabTypeId", "LabName");
            if (ModelState.IsValid)
            {
                UnitOfWork.LaboratoryOrderRepository.InsertLaboratoryOrderl(laboratoryOrder);
                return RedirectToAction("index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AppointmentPatientLabOrders(AppPatientOrdersVm appPatientLabVm)
        {

            var routeValues = new RouteValueDictionary(new { id = appPatientLabVm.AppointmentId });


            if (appPatientLabVm.ListofOrders != null)
            {

                foreach (var labID in appPatientLabVm.ListofOrders)
                {
                    UnitOfWork.LaboratoryOrderRepository.InsertLaboratoryOrderl(new LaboratoryOrder() { AppointmentId = appPatientLabVm.AppointmentId , LabTypeId = labID });

                }

            }
            return RedirectToAction("DetailedPatientAppointment", "Employee", routeValues);

        } 

        [HttpPost]
        public ActionResult AppointmentPatientLabOrdersEdit(AppPatientOrdersVm appPatientLabVm)
        {
            var ListOfOrders = UnitOfWork.LaboratoryOrderRepository.GetAllLaboratoryOrders().Where(lo=>lo.AppointmentId == appPatientLabVm.AppointmentId);

            foreach (var lab in ListOfOrders)
            {
                UnitOfWork.LaboratoryOrderRepository.DeleteLaboratoryOrder(lab.LaboratoryOrderId);
            }

            var routeValues = new RouteValueDictionary(new { id = appPatientLabVm.AppointmentId });


            if (appPatientLabVm.ListofOrders != null)
            {

                foreach (var labID in appPatientLabVm.ListofOrders)
                {
                    UnitOfWork.LaboratoryOrderRepository.InsertLaboratoryOrderl(new LaboratoryOrder() { AppointmentId = appPatientLabVm.AppointmentId , LabTypeId = labID });

                }

            }
            return RedirectToAction("DetailedPatientAppointment", "Employee", routeValues);

        }

        public ActionResult Edit(int id)
        {
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId", "AppointmentId");
            ViewBag.Labtypelst = new SelectList(UnitOfWork.LabratoryTypeRepository.GetAllLabTypes(), "LabTypeId", "LabName");

            return View(UnitOfWork.LaboratoryOrderRepository.GetLaboratoryOrderById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LaboratoryOrder laboratoryOrder)
        {
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId", "AppointmentId");
            ViewBag.Labtypelst = new SelectList(UnitOfWork.LabratoryTypeRepository.GetAllLabTypes(), "LabTypeId", "LabName");

            if (ModelState.IsValid)
            {
                UnitOfWork.LaboratoryOrderRepository.UpdateLaboratoryOrder(id, laboratoryOrder);
                return RedirectToAction("index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            UnitOfWork.LaboratoryOrderRepository.DeleteLaboratoryOrder(id);
            return RedirectToAction("index");
        }
    }
}

