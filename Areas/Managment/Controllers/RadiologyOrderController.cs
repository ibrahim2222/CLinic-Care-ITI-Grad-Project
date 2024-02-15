using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Clinc_Care_MVC_Grad_PROJ.Models.ViewModels;

namespace Resturant_RES_MVC_ITI_PRJ.Areas.Client.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Managment")]
    public class RadiologyOrderController : Controller
    {

        public IUnitOfWork UnitOfWork { get; }

        public RadiologyOrderController(IUnitOfWork unitOfWork)
        {

            UnitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {

            return View("Index", UnitOfWork.RadiologyOrderRepository.GetAllRadiologyOrders());
        }
     
        public ActionResult Details(int id)
        {
            return View("Details", UnitOfWork.RadiologyOrderRepository.GetRadiologyOrderById(id));
        }

        public ActionResult Create()
        {
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId", "AppointmentId");
            ViewBag.Radiologytypelst = new SelectList(UnitOfWork.RadiologyTypeRepository.GetAllRadiologyType(), "RadiologyTypeId", "RadiologyName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RadiologyOrder radiologyOrder)
        {
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId", "AppointmentId");
            ViewBag.Radiologytypelst = new SelectList(UnitOfWork.RadiologyTypeRepository.GetAllRadiologyType(), "RadiologyTypeId", "RadiologyName");

            if (ModelState.IsValid)
            {
                UnitOfWork.RadiologyOrderRepository.InsertRadiologyOrder(radiologyOrder);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public ActionResult AppointmentPatientRadiologyOrders(AppPatientOrdersVm appPatientLabVm)
        {

            var routeValues = new RouteValueDictionary(new { id = appPatientLabVm.AppointmentId });


            if (appPatientLabVm.ListofOrders != null)
            {

                foreach (var labID in appPatientLabVm.ListofOrders)
                {
                    UnitOfWork.RadiologyOrderRepository.InsertRadiologyOrder(new RadiologyOrder() { AppointmentId = appPatientLabVm.AppointmentId, RadiologyTypeId = labID });

                }

            }
            return RedirectToAction("DetailedPatientAppointment", "Employee", routeValues);

        }



        [HttpPost]
        public ActionResult AppointmentPatientRadiologyOrdersEdit(AppPatientOrdersVm appPatientLabVm)
        {
            var ListOfOrders = UnitOfWork.RadiologyOrderRepository.GetAllRadiologyOrders().Where(ro => ro.AppointmentId == appPatientLabVm.AppointmentId);

            foreach (var radiology in ListOfOrders)
            {
                UnitOfWork.RadiologyOrderRepository.DeleteRadiologyOrder(radiology.RadiologyOrderId);
            }

            var routeValues = new RouteValueDictionary(new { id = appPatientLabVm.AppointmentId });


            if (appPatientLabVm.ListofOrders != null)
            {

                foreach (var RadioID in appPatientLabVm.ListofOrders)
                {
                    UnitOfWork.RadiologyOrderRepository.InsertRadiologyOrder(new RadiologyOrder() { AppointmentId = appPatientLabVm.AppointmentId, RadiologyTypeId = RadioID });

                }

            }
            return RedirectToAction("DetailedPatientAppointment", "Employee", routeValues);

        }
        public ActionResult Edit(int id)
        {
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId", "AppointmentId");
            ViewBag.Radiologytypelst = new SelectList(UnitOfWork.RadiologyTypeRepository.GetAllRadiologyType(), "RadiologyTypeId", "RadiologyName");

            return View(UnitOfWork.RadiologyOrderRepository.GetRadiologyOrderById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RadiologyOrder radiologyOrder)
        {
            ViewBag.Appointmentlst = new SelectList(UnitOfWork.AppointmentRepository.GetAllAppointments(), "AppointmentId", "AppointmentId");
            ViewBag.Radiologytypelst = new SelectList(UnitOfWork.RadiologyTypeRepository.GetAllRadiologyType(), "RadiologyTypeId", "RadiologyName");

            if (ModelState.IsValid)
            {
                UnitOfWork.RadiologyOrderRepository.UpdateRadiologyOrder(id, radiologyOrder);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        //public ActionResult Delete(int id)
        //{
        //    UnitOfWork.RadiologyOrderRepository.DeleteRadiologyOrder(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}

