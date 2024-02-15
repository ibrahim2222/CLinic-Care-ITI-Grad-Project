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
    public class LaboratoryTypeController : Controller
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public LaboratoryTypeController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View(UnitOfWork.LabratoryTypeRepository.GetAllLabTypes());
        }

        public ActionResult Details(int id)
        {
            return View(UnitOfWork.LabratoryTypeRepository.GetLabTypeById(id));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LaboratoryType laboratoryType)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.LabratoryTypeRepository.InsertLabType(laboratoryType);
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
        public ActionResult Edit(int id)
        {
            return View(UnitOfWork.LabratoryTypeRepository.GetLabTypeById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LaboratoryType laboratoryType)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.LabratoryTypeRepository.UpdateLabType(id, laboratoryType);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        // GET: LaboratoryTypeController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    UnitOfWork.LabratoryTypeRepository.DeleteLabType(id);
        //    return RedirectToAction(nameof(Index));
        //}


    }
}
