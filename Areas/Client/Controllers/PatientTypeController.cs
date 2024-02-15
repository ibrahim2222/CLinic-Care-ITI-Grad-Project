using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Client.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Client")]
    public class PatientTypeController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }
        public PatientTypeController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View("Index", UnitOfWork.PatientTypeRepository.GetAllPatientTypes());
        }

        public ActionResult Details(int id)
        {
            return View("Details", UnitOfWork.PatientTypeRepository.GetPatientTypeById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientType patientType)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.PatientTypeRepository.InsertPatientType(patientType);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(UnitOfWork.PatientTypeRepository.GetPatientTypeById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PatientType patientType)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.PatientTypeRepository.UpdatePatientType(id, patientType);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        //public ActionResult Delete(int id)
        //{
        //    UnitOfWork.PatientTypeRepository.DeletePatientType(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
