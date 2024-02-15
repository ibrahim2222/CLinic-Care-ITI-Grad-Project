using Clinc_Care_MVC_Grad_PROJ.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Clinc_Care_MVC_Grad_PROJ.Models.ViewModels;
using Clinc_Care_MVC_Grad_PROJ.EmailServices;
using Message = Clinc_Care_MVC_Grad_PROJ.EmailServices.Message;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client;
using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;

namespace Clinc_Care_MVC_Grad_PROJ.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IEmailSender emailSender;

        public IUnitOfWork UnitOfWork { get; }

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender,
            IUnitOfWork unitOfWork
          )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            UnitOfWork = unitOfWork;
        }

/*        [HttpGet]
        public IActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }*/

        [HttpPost]
        public async Task<IActionResult> Register(CombinedVM registerUserVM)
        {
            var phones = userManager.Users.Select(u => u.PhoneNumber).ToList();
            if (phones.Contains(registerUserVM.RegModel.Phone))
            {
                ModelState.AddModelError("Phone", "Already Exist Phone Number");
            }
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.FirstName = registerUserVM.RegModel.FirstName;
                user.LastName = registerUserVM.RegModel.LastName;
                user.UserName = registerUserVM.RegModel.Email;
                user.Email = registerUserVM.RegModel.Email;
                user.PhoneNumber = registerUserVM.RegModel.Phone;
                user.PasswordHash = registerUserVM.RegModel.Password;
                IdentityResult result = await userManager.CreateAsync(user, registerUserVM.RegModel.Password);

                Patient patient = new Patient()
                {
                    PatientFirstName = user.FirstName,
                    PatientLastName = user.LastName,
                    PatientEmail = user.Email,
                    PatientPassword = user.PasswordHash,
                    PatientPhone = user.PhoneNumber,
                    PatientTypeId=2,
                };

                // Create a new claim to add to the user
                var claim = new Claim(ClaimTypes.Name, user.Email); // Replace with your own claim type and value


                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, claim);

                    await userManager.AddToRoleAsync(user, "Client");
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email, username = user.UserName }, Request.Scheme);
                    var message = new Message(new string[] { user.Email }, "Confirmation email link", $"<a style='background-color:  #FFBC32;\r\n  border-color: #FFBC32;\r\nborder-radius: 3px;\r\nborder-style: solid;\r\ntext-decoration: none; \r\ntext-decoration-line: none;\r\n display: inline-block;\r\n  font-weight: 400;\r\n  line-height: 1.5;\r\n  color: #212529;\r\n  text-align: center;\r\n  text-decoration: none;\r\n  vertical-align: middle;\r\n  cursor: pointer;\r\n  user-select: none;\r\n  padding: 10px;\r\n  font-size: 16px;\r\n  border-radius: 0.25rem;'  href='{confirmationLink}'>Click Here</a> ", null);
                    await emailSender.SendEmailAsync(message);
                    UnitOfWork.PatientRepository.InsertPatient(patient);
                    return RedirectToAction(nameof(SuccessRegistration));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("all", error.Description);
                    }
                }
            }
            return View("Login",registerUserVM);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email, string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return View("Error");
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SuccessRegistration()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(CombinedVM userVM)
        {
            AppUser userFromDB = await userManager.FindByEmailAsync(userVM.LogModel.LoginEmail);

            if (userFromDB != null)
            {
                bool result = await userManager.IsEmailConfirmedAsync(userFromDB);

                if (result == true)
                {
                    bool exist = await userManager.CheckPasswordAsync(userFromDB, userVM.LogModel.LoginPassword);

                    if (exist == true)
                    {
                        await signInManager.SignInAsync(userFromDB, userVM.LogModel.RemeberMe);
                      var role =  await userManager.GetRolesAsync(userFromDB);
                        if (role.Contains("Client"))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("DbIndex", "Home");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login Attempt Please confirm your email");
                    return View();
                }
            }
            ModelState.AddModelError("error", "Not Correct UserName Or Password Try again");
            return View(userVM);
        }


        [HttpGet]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var act = provider == "google" ? nameof(ExternalLoginCallback) : nameof(FacebookCallback);
            var redirectUrl = Url.Action(act, "Account", new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        //GoogleExternal

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
        {
            
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }
            var userEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
            var userexist = await userManager.FindByEmailAsync(userEmail);
                if (userexist != null) {
               var Logins = await userManager.GetLoginsAsync(userexist);
                if (userManager.Users.Select(u => u.Email).Contains(userEmail) && Logins.Any())
                {
                    var UserExternalLoginData = Logins.Select(e => new { LoginProviderName = e.LoginProvider, LoginProviderKey = e.ProviderKey }).FirstOrDefault();
                    info.LoginProvider = UserExternalLoginData.LoginProviderName;
                    info.ProviderKey = UserExternalLoginData.LoginProviderKey;
                }
                else
                {
                    return View("ExternalLogin");
                }
            }
            
            
            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            if (signInResult.IsLockedOut)
            {
                return RedirectToAction(nameof(signInManager));
            }
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["Provider"] = info.LoginProvider;

                var user = new AppUser
                {
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                    FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                    LastName = info.Principal.FindFirstValue(ClaimTypes.Surname),
                    EmailConfirmed = true
                };

                Patient patient = new Patient()
                {
                    PatientFirstName = user.FirstName,
                    PatientLastName = user.LastName,
                    PatientEmail = user.Email,
                    PatientTypeId = 2,

                };
                var createResult = await userManager.CreateAsync(user);

                // Create a new claim to add to the user
                var claim = new Claim(ClaimTypes.Name, user.Email); // Replace with your own claim type and value

            

                if (createResult.Succeeded )
                {
                    await userManager.AddClaimAsync(user, claim);
                    await userManager.AddToRoleAsync(user, "Client");
                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    var message = new Message(new string[] { user.Email }, "Welcome", null, null);
                    await emailSender.SendEmailAsync(message);
                    UnitOfWork.PatientRepository.InsertPatient(patient);
                    return RedirectToLocal(returnUrl);
                }
                return View("ExternalLogin", new ExternalLoginModel { Email = user.UserName });
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "home");
            }
        }


        //FacebookExternal

        [HttpGet]

        public async Task<IActionResult> FacebookCallback(string returnUrl = null)
        {

            Microsoft.AspNetCore.Identity.SignInResult signInResult;
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("Login");
            }
            if (info.Principal.FindFirstValue(ClaimTypes.Email) == null)
            {
                return View("ExternalloginFacebook");
            }
            var userEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
            var userexist = await userManager.FindByEmailAsync(userEmail);
            if (userexist != null)
            {
                var Logins = await userManager.GetLoginsAsync(userexist);
                if (userManager.Users.Select(u => u.Email).Contains(userEmail) && Logins.Any())
                {
                    var UserExternalLoginData = Logins.Select(e => new { LoginProviderName = e.LoginProvider, LoginProviderKey = e.ProviderKey }).FirstOrDefault();
                    info.LoginProvider = UserExternalLoginData.LoginProviderName;
                    info.ProviderKey = UserExternalLoginData.LoginProviderKey;
                }
                else
                {
                    return View("ExternalLoginFacebook");
                }
            }

            signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            if (signInResult.IsLockedOut)
            {
                return RedirectToAction("login");
            }
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["Provider"] = info.LoginProvider;

                var user = new AppUser
                {

                    Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                    FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                    LastName = info.Principal.FindFirstValue(ClaimTypes.Surname),
                    EmailConfirmed = true
                };

                Patient patient = new Patient()
                {
                    PatientFirstName = user.FirstName,
                    PatientLastName = user.LastName,
                    PatientEmail = user.Email,
                    PatientTypeId = 2,
                };
                var createResult = await userManager.CreateAsync(user);

                // Create a new claim to add to the user
                var claim = new Claim(ClaimTypes.Name, user.Email); // Replace with your own claim type and value

          

                if (createResult.Succeeded)
                {
                    await userManager.AddClaimAsync(user, claim);
                    await userManager.AddToRoleAsync(user, "Client");
                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    var message = new Message(new string[] { user.Email }, "Welcome", null, null);
                    await emailSender.SendEmailAsync(message);
                    UnitOfWork.PatientRepository.InsertPatient(patient);
                    return RedirectToLocal(returnUrl);
                }

                return View("ExternalloginFacebook");
            }
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
           var patient = UnitOfWork.PatientRepository.GetAllPatients().Where(p => p.PatientEmail == forgotPasswordModel.Email).SingleOrDefault();

            if (!ModelState.IsValid)
            {
                return View(forgotPasswordModel);
            }
            var user = await userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }else if (user.Email.ToLower().Contains("@roshetaclinic.com")&&patient!=null)
            {
                var routeValues = new RouteValueDictionary(new { area = "Client" ,id=patient.PatientId});
                return RedirectToAction("changepassword","Patient",routeValues);
            }
           
            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
            var content = $"<a style='background-color:  #FFBC32;\r\n  border-color: #FFBC32;\r\nborder-radius: 3px;\r\nborder-style: solid;\r\ntext-decoration: none; \r\ntext-decoration-line: none;\r\n display: inline-block;\r\n  font-weight: 400;\r\n  line-height: 1.5;\r\n  color: #212529;\r\n  text-align: center;\r\n  text-decoration: none;\r\n  vertical-align: middle;\r\n  cursor: pointer;\r\n  user-select: none;\r\n  padding: 10px;\r\n  font-size: 16px;\r\n  border-radius: 0.25rem;'  href='{callback}'>Click Here</a> ";
            var message = new Message(new string[] { user.Email }, "Reset your password at Clinic", content, null);
            await emailSender.SendEmailAsync(message);
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordModel);
            var user = await userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                return View();
            var resetPassResult = await userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }
            else
            {
                var patient = UnitOfWork.PatientRepository.GetAllPatients().Where(p => p.PatientEmail == user.Email).SingleOrDefault();
                patient.PatientPassword = user.PasswordHash;
                UnitOfWork.Savechanges();
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult Profile()
        //{
        //    var UserData = userManager.Users.FirstOrDefault(e => e.UserName == User.Identity.Name);
        //    if (User.IsInRole("Customer"))
        //    {
        //        return View(UserData);
        //    }
        //    return View("EmployeeProfile", UserData);
        //}


        public ActionResult MyAppointments(int id)
        {
            return View(UnitOfWork.AppointmentRepository.GetAllAppointments().Where(p => p.PatientId == 1));
        }
        public ActionResult CancelAppointment(int id)
        {
            UnitOfWork.AppointmentRepository.DeleteAppointment(id,false);
            var data = UnitOfWork.AppointmentRepository.GetAppointmentById(id).PatientId;
            return RedirectToAction($"MyAppointments/{data}");

        }
    }
}