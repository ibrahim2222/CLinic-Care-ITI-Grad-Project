using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Managment")]
    public class DoctorSpecializionController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }

        public DoctorSpecializionController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View("Index", UnitOfWork.DoctorSpecializionRepository.GetAllDoctorSpecializion());
            // ViewBag.EmpTypes = new SelectList(UnitOfWork.DoctorSpecializionRepository.GetAllDoctorSpecializion(), "SpecializationId", "SpecializationName");

        }

        public ActionResult Details(int id)
        {
            return View("Details", UnitOfWork.DoctorSpecializionRepository.GetDoctorSpecializionById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorSpecializion doctorSpecializion)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.DoctorSpecializionRepository.InsertDoctorSpecializion(doctorSpecializion);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public ActionResult Edit(int id)
        {
            return View(UnitOfWork.DoctorSpecializionRepository.GetDoctorSpecializionById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DoctorSpecializion doctorSpecializion)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.DoctorSpecializionRepository.UpdateDoctorSpecializion(id, doctorSpecializion);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        /*
           public ActionResult Delete(int id)
           {
               UnitOfWork.DoctorSpecializionRepository.DeleteDoctorSpecializion(id);
               return RedirectToAction(nameof(Index));
           }*/
    }
}
