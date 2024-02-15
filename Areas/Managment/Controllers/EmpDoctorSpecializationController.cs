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
    public class EmpDoctorSpecializationController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }
        public EmpDoctorSpecializationController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View("Index", UnitOfWork.EmpDoctorSpecializionRepository.GetAllEmpDoctorSpecilzations());

        }
        public ActionResult Details(int id)
        {
            return View("Details", UnitOfWork.EmpDoctorSpecializionRepository.GetEmpDoctorSpecilzationById(id));

        }
        public ActionResult Create()
        {
            ViewBag.DoctorSpecilzations = new SelectList(UnitOfWork.DoctorSpecializionRepository.GetAllDoctorSpecializion(), "SpecializationId", "SpecializationName");
            ViewBag.Doctors = new SelectList(UnitOfWork.EmployeeRepository.GetAllEmployees().Where(emp => emp.EmployeeTypeIdNavigation.TypeName == "Doctor"), "EmpId", "EmployeeFullName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmpDoctorSpecilzation empDoctorSpecilzation)
        {
            ViewBag.DoctorSpecilzations = new SelectList( UnitOfWork.DoctorSpecializionRepository.GetAllDoctorSpecializion(), "SpecializationId", "SpecializationName");
            ViewBag.Doctors = new SelectList(UnitOfWork.EmployeeRepository.GetAllEmployees().Where(emp => emp.EmployeeTypeIdNavigation.TypeName == "Doctor"), "EmpId", "EmployeeFullName");

            if (ModelState.IsValid)
            {
                UnitOfWork.EmpDoctorSpecializionRepository.InsertEmpDoctorSpecilzation(empDoctorSpecilzation);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.DoctorSpecilzations = new SelectList(UnitOfWork.DoctorSpecializionRepository.GetAllDoctorSpecializion(), "SpecializationId", "SpecializationName");
            ViewBag.Doctors = new SelectList(UnitOfWork.EmployeeRepository.GetAllEmployees().Where(emp => emp.EmployeeTypeIdNavigation.TypeName == "Doctor"), "EmpId", "EmployeeFullName");

            return View(UnitOfWork.EmpDoctorSpecializionRepository.GetEmpDoctorSpecilzationById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmpDoctorSpecilzation empDoctorSpecilzation)
        {
            ViewBag.DoctorSpecilzations = new SelectList(UnitOfWork.DoctorSpecializionRepository.GetAllDoctorSpecializion(), "SpecializationId", "SpecializationName");
            ViewBag.Doctors = new SelectList(UnitOfWork.EmployeeRepository.GetAllEmployees().Where(emp => emp.EmployeeTypeIdNavigation.TypeName == "Doctor"), "EmpId", "EmployeeFullName");

            if (ModelState.IsValid)
            {
                UnitOfWork.EmpDoctorSpecializionRepository.UpdateEmpDoctorSpecilzation(id, empDoctorSpecilzation);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        //public ActionResult Delete(int id)
        //{
        //    UnitOfWork.EmpDoctorSpecializionRepository.DeleteEmpDoctorSpecilzation(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
