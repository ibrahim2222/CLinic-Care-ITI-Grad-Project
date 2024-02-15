using Clinc_Care_MVC_Grad_PROJ.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.RepoServices;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Clinc_Care_MVC_Grad_PROJ.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IInitializeDefaultData InitializeDefaultData { get; }
        public UserManager<AppUser> UserManager { get; }

        public HomeController(ILogger<HomeController> logger, IInitializeDefaultData initializeDefaultData, UserManager<AppUser> userManager)
        {
            _logger = logger;
            InitializeDefaultData = initializeDefaultData;
            UserManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (UserManager.Users.Any())
            {
                return View();
            }
            await InitializeDefaultData.Initialize_Data();

            return View();
        }
        [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]
        public IActionResult DbIndex()
        {
            return View();
        }

        [Authorize(Roles = "Client")]
        public IActionResult Profile()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}