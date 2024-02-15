using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Managment")]
    public class RadiologyRecordController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }
        public RadiologyRecordController(IUnitOfWork unitOfWork)

        {
            UnitOfWork = unitOfWork;

        }
        public ActionResult Index()
        {
            return View(UnitOfWork.RadiologyRecordRepository.GetAllRadiologyRecord());
        }

        public ActionResult Details(int id)
        {
            return View(UnitOfWork.RadiologyRecordRepository.GetRadiologyRecordById(id));
        }

        public ActionResult Create()
        {
            ViewBag.RadiologyTypes = new SelectList(UnitOfWork.RadiologyTypeRepository.GetAllRadiologyType(), "RadiologyTypeId", "RadiologyName");
            ViewBag.Patients = new SelectList(UnitOfWork.PatientRepository.GetAllPatients(), "PatientId", "PatientFullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RadiologyRecord radiologyRecord)
        {
            ViewBag.RadiologyTypes = new SelectList(UnitOfWork.RadiologyTypeRepository.GetAllRadiologyType(), "RadiologyTypeId", "RadiologyName");
            ViewBag.Patients = new SelectList(UnitOfWork.PatientRepository.GetAllPatients(), "PatientId", "PatientFullName");

            if (radiologyRecord != null)
            {
                UnitOfWork.RadiologyRecordRepository.InsertRadiologyRecord(radiologyRecord);
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
        public ActionResult Edit(int id)
        {
            ViewBag.RadiologyTypes = new SelectList(UnitOfWork.RadiologyTypeRepository.GetAllRadiologyType(), "RadiologyTypeId", "RadiologyName");
            ViewBag.Patients = new SelectList(UnitOfWork.PatientRepository.GetAllPatients(), "PatientId", "PatientFullName");
            return View(UnitOfWork.RadiologyRecordRepository.GetRadiologyRecordById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RadiologyRecord radiologyRecord)
        {
            ViewBag.RadiologyTypes = new SelectList(UnitOfWork.RadiologyTypeRepository.GetAllRadiologyType(), "RadiologyTypeId", "RadiologyName");
            ViewBag.Patients = new SelectList(UnitOfWork.PatientRepository.GetAllPatients(), "PatientId", "PatientFullName");

            if (ModelState.IsValid)
            {
                UnitOfWork.RadiologyRecordRepository.UpdateRadiologyRecord(id, radiologyRecord);
                return RedirectToAction(nameof(Index));
            }

            return View();

        }
        //public ActionResult Delete(int id)
        //{
        //    UnitOfWork.RadiologyRecordRepository.DeleteRadiologyRecord(id);
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
