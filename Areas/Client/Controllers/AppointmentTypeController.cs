using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    public class AppointmentTypeController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }
        public AppointmentTypeController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

        }
        public ActionResult Index()
        {
            return View("Index", UnitOfWork.AppointmentTypeRepository.GetAllAppointmentTypes());
        }
        public ActionResult Details(int id)
        {
            return View(UnitOfWork.AppointmentTypeRepository.GetAppointmentTypeById(id));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentType appointmentType)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.AppointmentTypeRepository.InsertAppointmentType(appointmentType);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            return View(UnitOfWork.AppointmentTypeRepository.GetAppointmentTypeById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AppointmentType appointmentType)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.AppointmentTypeRepository.UpdateAppointmentType(id, appointmentType);
                return RedirectToAction("Index");
            }
            return View();
        }
        //public ActionResult Delete(int id)
        //{
        //    UnitOfWork.AppointmentTypeRepository.DeleteAppointmentType(id);
        //    return RedirectToAction("Index");
        //}
    }
}
