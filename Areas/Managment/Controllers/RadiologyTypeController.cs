using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Managment")]
    public class RadiologyTypeController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }

        public RadiologyTypeController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {

            return View(UnitOfWork.RadiologyTypeRepository.GetAllRadiologyType());
        }

        public ActionResult Details(int id)
        {
            return View(UnitOfWork.RadiologyTypeRepository.GetRadiologyTypeById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RadiologyType radiologyType)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.RadiologyTypeRepository.InsertRadiologyType(radiologyType);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            return View(UnitOfWork.RadiologyTypeRepository.GetRadiologyTypeById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RadiologyType radiologyType)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.RadiologyTypeRepository.UpdateRadiologyType(id, radiologyType);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        /*public ActionResult Delete(int id)
                {
                    UnitOfWork.RadiologyTypeRepository.DeleteRadiologyType(id);
                    return RedirectToAction(nameof(Index));
                }*/
    }
}
