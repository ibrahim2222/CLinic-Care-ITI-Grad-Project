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
    public class MedicineController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }


        public MedicineController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View("Index", UnitOfWork.MedicineRepository.GetAllMedicines());
        }
        public ActionResult Details(int id)
        {
            return View("Details", UnitOfWork.MedicineRepository.GetMedicineById(id));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.MedicineRepository.InsertMedicine(medicine);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public ActionResult Edit(int id)
        {
            return View(UnitOfWork.MedicineRepository.GetMedicineById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.MedicineRepository.UpdateMedicine(id, medicine);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        //public ActionResult Delete(int id)
        //{
        //    UnitOfWork.MedicineRepository.DeleteMedicine(id);
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
