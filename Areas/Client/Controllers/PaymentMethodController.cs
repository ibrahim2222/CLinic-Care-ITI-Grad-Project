using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Clinc_Care_MVC_Grad_PROJ.Areas.Client.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Client")]
    public class PaymentMethodController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }
        public PaymentMethodController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

        }
        public ActionResult Index()
        {
            return View("Index", UnitOfWork.PaymentMethodRepository.GetAllPaymentMethods());
        }
        //public ActionResult Index(IFormCollection collection)
        //{
        //    int PaymentMethodId = int.Parse(collection["PaymentMethodId"]);
        //    ViewBag.PaymentMethodsList = new SelectList(PaymentMethodRepository.GetAllPaymentMethods(), "PaymentMethodId", "PaymentMethodName", PaymentMethodId);

        //    return View(PaymentMethodRepository.GetAllPaymentMethods().Where(d => d.PaymentMethodId == PaymentMethodId));
        //}
        public ActionResult Details(int id)
        {
            return View(UnitOfWork.PaymentMethodRepository.GetPaymentMethodById(id));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.PaymentMethodRepository.InsertPaymentMethod(paymentMethod);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(UnitOfWork.PaymentMethodRepository.GetPaymentMethodById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.PaymentMethodRepository.UpdatePaymentMethod(id,  paymentMethod);
                return RedirectToAction("Index");
            }
            return View();
        }
        //public ActionResult Delete(int id)
        //{
        //    UnitOfWork.PaymentMethodRepository.DeletePaymentMethod(id);
        //    return RedirectToAction("Index");
        //}
    }
}
