using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Managment")]
    public class DosageController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }

        public DosageController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View("Index", UnitOfWork.DosageRepository.GetAllDosages());
        }

        public ActionResult Details(int id)
        {
            return View("Details", UnitOfWork.DosageRepository.GetDosageById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dosage dosage)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.DosageRepository.InsertDosage(dosage);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dosage dosage)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.DosageRepository.UpdateDosage(id, dosage);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        /*        public ActionResult Delete(int id)
                {
                    UnitOfWork.DosageRepository.DeleteDosage(id);
                    return RedirectToAction(nameof(Index));
                }*/


    }
}
