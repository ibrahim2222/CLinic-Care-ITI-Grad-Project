using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.EmailServices;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Controllers
{
    [Area("Managment")]
    [Authorize(Roles = "Admin,Examination,Doctor")]

    public class RadRequestController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }
        public IWebHostEnvironment Environment { get; }
        public IEmailSender EmailSender { get; }

        public RadRequestController(IUnitOfWork unitOfWork, IWebHostEnvironment _environment, IEmailSender emailSender)
        {
            UnitOfWork = unitOfWork;
            Environment = _environment;
            EmailSender = emailSender;
        }
        public ActionResult Index()
        {
            return View("Index", UnitOfWork.RadiologyOrderRepository.GetAllRadiologyOrders());
        }

        public ActionResult ResultFormModal(int id)
        {
            return PartialView("_AppointmentRadPdf", UnitOfWork.AppointmentRepository.GetAppointmentById(id));
        }

        [HttpPost]
        public IActionResult UploadPdf(IFormFile pdfFile, int id)
        {
            if (pdfFile != null && pdfFile.Length > 0)
            {
                try
                {
                    var wwwrootPath = Environment.WebRootPath;
                    var RadResultFolder = Path.Combine(wwwrootPath, "RadResult");

                    if (!Directory.Exists(RadResultFolder))
                    {
                        Directory.CreateDirectory(RadResultFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + pdfFile.FileName;
                    var filePath = Path.Combine(RadResultFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        pdfFile.CopyTo(stream);
                    }
                    var radrec = UnitOfWork.RadiologyRecordRepository.GetAllRadiologyRecord().Where(c => c.AppointmentId == id).SingleOrDefault();
                    if (radrec == null)
                    {
                        var rad = new RadiologyRecord() { AppointmentId = id, RadioResult = uniqueFileName };
                        UnitOfWork.RadiologyRecordRepository.InsertRadiologyRecord(rad);

                    }
                    else
                    {
                        radrec.RadioResult = uniqueFileName;
                        UnitOfWork.RadiologyRecordRepository.UpdateRadiologyRecord(radrec.RadiologyRecordId, radrec);
                    }
                    var routeValues = new RouteValueDictionary(new { pdfFile = uniqueFileName });
                    //IFormFileCollection formFiles = new FormFileCollection() { };
                    var email = UnitOfWork.AppointmentRepository.GetAppointmentById(id).PatientIdNavigation.PatientEmail;



                    var message = new Message(new string[] { email }, "Clinic Appointment Rad Results", $"<h3>Hello,Your Appointment Result Just Arrived", new FormFileCollection() { pdfFile });
                    EmailSender.SendEmail(message);
                    return RedirectToAction("Pdf", "RadRequest", routeValues);
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
            ViewBag.pdf = pdfFile;
            return View();
        }
    }
}
