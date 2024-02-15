using Clinc_Care_MVC_Grad_PROJ.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Managment")]
    public class DeletedEmpDoctorSpecController : Controller
    {
        public ClinicDbContext Ctx { get; }

        public DeletedEmpDoctorSpecController(ClinicDbContext Ctx)
        {
            this.Ctx = Ctx;
        }
        public ActionResult Index()
        {
            return View(Ctx.DeletedEmpDoctorSpecilzations.ToList());
        }
    }
}
