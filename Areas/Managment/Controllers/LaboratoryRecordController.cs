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
    public class LaboratoryRecordController : Controller
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public LaboratoryRecordController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View(UnitOfWork.LaboratoryRecordRepository.GetAllLabRecords());
        }
        public ActionResult Details(int id)
        {
            return View(UnitOfWork.LaboratoryRecordRepository.GetLabRecordById(id));
        }
        public ActionResult Create()
        {

            ViewBag.LabTypes = new SelectList(UnitOfWork.LabratoryTypeRepository.GetAllLabTypes(), "LabTypeId", "LabName");
            ViewBag.Patients = new SelectList(UnitOfWork.PatientRepository.GetAllPatients(), "PatientId", "PatientFullName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LaboratoryRecord laboratoryRecord)
        {
            ViewBag.LabTypes = new SelectList(UnitOfWork.LabratoryTypeRepository.GetAllLabTypes(), "LabTypeId", "LabName");
            ViewBag.Patients = new SelectList(UnitOfWork.PatientRepository.GetAllPatients(), "PatientId", "PatientFullName");
            if (ModelState.IsValid)
            {
                UnitOfWork.LaboratoryRecordRepository.InsertLabRecord(laboratoryRecord);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.LabTypes = new SelectList(UnitOfWork.LabratoryTypeRepository.GetAllLabTypes(), "LabTypeId", "LabName");
            ViewBag.Patients = new SelectList(UnitOfWork.PatientRepository.GetAllPatients(), "PatientId", "PatientFullName"); return View(UnitOfWork.LaboratoryRecordRepository.GetLabRecordById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LaboratoryRecord laboratoryRecord)
        {
            ViewBag.LabTypes = new SelectList(UnitOfWork.LabratoryTypeRepository.GetAllLabTypes(), "LabTypeId", "LabName");
            ViewBag.Patients = new SelectList(UnitOfWork.PatientRepository.GetAllPatients(), "PatientId", "PatientFullName");
            if (ModelState.IsValid)
            {
                UnitOfWork.LaboratoryRecordRepository.UpdateLabRecord(id, laboratoryRecord);
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
        // GET: LaboratoryRecordController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    UnitOfWork.LaboratoryRecordRepository.DeleteLabRecord(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
