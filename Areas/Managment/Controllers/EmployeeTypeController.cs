using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Managment")]
    public class EmployeeTypeController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }
        public EmployeeTypeController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View("Index", UnitOfWork.EmployeeTypeRepository.GetAllEmployeeType());
        }


        public ActionResult Details(int id)
        {
            return View("Details", UnitOfWork.EmployeeTypeRepository.GetEmployeeTypeById(id));
        }


        public ActionResult Create()
        {
            // ViewBag.EmpTypes = new SelectList(UnitOfWork.EmployeeTypeRepository.GetAllEmployeeType(), "EmployeeTypeId", "TypeName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeType employeeType)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.EmployeeTypeRepository.InsertEmployeeType(employeeType);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            return View(UnitOfWork.EmployeeTypeRepository.GetEmployeeTypeById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeType employeeType)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.EmployeeTypeRepository.UpdateEmployeeType(id, employeeType);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        /* public ActionResult Delete(int id)
         {
             UnitOfWork.EmployeeTypeRepository.DeleteEmployeeType(id);
             return RedirectToAction(nameof(Index));
         }*/


    }
}
