using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Stripe;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Client.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Client")]
    public class FeeController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }
        public FeeController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View("Index", UnitOfWork.FeeRepository.GetAllFees());
        }

        // GET: FeeController/Details/5
        public ActionResult Details(int id)
        {
            return View(UnitOfWork.FeeRepository.GetFeeById(id));
        }

        // GET: FeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fee fee)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.FeeRepository.InsertFee(fee);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: FeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(UnitOfWork.FeeRepository.GetFeeById(id));
        }

        // POST: FeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Fee fee)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.FeeRepository.UpdateFee(id, fee);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: FeeController/Delete/5
        public ActionResult Delete(int id)
        {
            UnitOfWork.FeeRepository.DeleteFee(id);
            return RedirectToAction("Index");
        }
    }
}
