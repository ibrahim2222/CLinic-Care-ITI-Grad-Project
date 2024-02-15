using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.EmailServices;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Controllers
{
    [Area("Managment")]
    public class LabRequestController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }
        public IWebHostEnvironment Environment { get; }
        public IEmailSender EmailSender { get; }

        public LabRequestController(IUnitOfWork unitOfWork, IWebHostEnvironment _environment,IEmailSender emailSender)
        {
            UnitOfWork = unitOfWork;
            Environment = _environment;
            EmailSender = emailSender;
        }
        public ActionResult Index()
        {
            return View("Index", UnitOfWork.LaboratoryOrderRepository.GetAllLaboratoryOrders());
        }

        public ActionResult ResultFormModal(int id)
        {
            return PartialView("_AppointmentLapPdf", UnitOfWork.AppointmentRepository.GetAppointmentById(id));
        }

        [HttpPost]
        public IActionResult UploadPdf(IFormFile pdfFile, int id)
        {
            if (pdfFile != null && pdfFile.Length > 0)
            {
                try
                {
                    var wwwrootPath = Environment.WebRootPath;
                    var LabResultFolder = Path.Combine(wwwrootPath, "LabResult");

                    if (!Directory.Exists(LabResultFolder))
                    {
                        Directory.CreateDirectory(LabResultFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + pdfFile.FileName;
                    var filePath = Path.Combine(LabResultFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        pdfFile.CopyTo(stream);
                    }
                    var labrec = UnitOfWork.LaboratoryRecordRepository.GetAllLabRecords().Where(c => c.AppointmentId == id).SingleOrDefault();
                    if (labrec == null)
                    {
                        var lab = new LaboratoryRecord() { AppointmentId = id, LabResult = uniqueFileName };
                        UnitOfWork.LaboratoryRecordRepository.InsertLabRecord(lab);

                    }
                    else { 
                        labrec.LabResult = uniqueFileName;
                        UnitOfWork.LaboratoryRecordRepository.UpdateLabRecord(labrec.LaboratoryRecordId ,labrec);
                    }
                    var routeValues = new RouteValueDictionary(new { pdfFile = uniqueFileName });
                    //IFormFileCollection formFiles = new FormFileCollection() { };
                    var email = UnitOfWork.AppointmentRepository.GetAppointmentById(id).PatientIdNavigation.PatientEmail;



                    var message = new Message(new string[] { email }, "Clinic Appointment Lab Results", $"<h3>Hello,Your Appointment Result Just Arrived", new FormFileCollection() { pdfFile });
                     EmailSender.SendEmail(message);
                    return RedirectToAction("Pdf","LabRequest", routeValues);
                }
                catch (Exception ex)
                {
                    return Content($"Error: {ex.Message}");
                }
            }

            return Content("Please select a PDF file to upload.");
        }
        public IActionResult Pdf(string pdfFile)
        {
            ViewBag.pdf=pdfFile;
            return View();
        }
    }
}
