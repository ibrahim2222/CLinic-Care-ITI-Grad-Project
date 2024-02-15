using Castle.Core.Resource;
using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Clinc_Care_MVC_Grad_PROJ.Models.ViewModels;
using Clinc_Care_MVC_Grad_PROJ.whatsappService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Stripe;
using Microsoft.AspNetCore.Authorization;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Client.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Client")]

    public class PatientController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }
        public UserManager<AppUser> UserManager { get; }
        public ITwilioService TwilioService { get; }

        public PatientController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager,ITwilioService twilioService)
        {
            UnitOfWork = unitOfWork;
            UserManager = userManager;
            TwilioService = twilioService;
        }

        public ActionResult Index()
        {
            return View("Index", UnitOfWork.PatientRepository.GetAllPatients());
        }

        public ActionResult Details(int id)
        {
            return View(UnitOfWork.PatientRepository.GetPatientById(id));
        }

        public ActionResult Create()
        {
            ViewBag.PatientTypeList = new SelectList(UnitOfWork.PatientTypeRepository.GetAllPatientTypes(), "PatientTypeId", "PatientTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Patient patient)
        {
            ViewBag.PatientTypeList = new SelectList(UnitOfWork.PatientTypeRepository.GetAllPatientTypes(), "PatientTypeId", "PatientTypeName");
            if (patient.PatientPhone == null)
            {
                ModelState.AddModelError("PatientPhone", "You must enter phone number");
            }
            else
            {
                var phones = UnitOfWork.PatientRepository.GetAllPatients().Any(c => c.PatientPhone == patient.PatientPhone);
                if (phones)
                {
                    ModelState.AddModelError("PatientPhone", "Already Exist Phone Number");
                }
            }
            if (ModelState.IsValid)
            {
                patient.PatientPassword = $"{patient.PatientFirstName}#{patient.PatientPhone}";
                string capitalizedFirtsChar = char.ToUpper(patient.PatientPassword[0]) + patient.PatientPassword.Substring(1);
                var user = new AppUser();
           
                user.FirstName = patient.PatientFirstName;
                user.LastName = patient.PatientLastName;

                if (patient.PatientEmail != null)
                {
                    user.UserName = patient.PatientEmail;
                    user.Email = patient.PatientEmail;
                }
                else
                {
                    Random random = new Random();
                    int randomNum = random.Next(1, 201);
                    long newNum = Convert.ToInt64(2 + patient.PatientPhone) / 250000 * 2;
                    var domain = $"{patient.PatientFirstName}_{newNum}@roshetaclinic.com";
                    user.UserName = domain;
                    user.Email = domain;
                }
                user.EmailConfirmed= true;
                user.PhoneNumber = patient.PatientPhone;
                user.PasswordHash = capitalizedFirtsChar;
                var Result = await UserManager.CreateAsync(user, capitalizedFirtsChar);
                if (Result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "Client");
                    patient.PatientEmail = user.Email;
                    patient.PatientPassword = user.PasswordHash;
                    UnitOfWork.PatientRepository.InsertPatient(patient);
                    //sendmessage
                    await TwilioService.SendMessageAsync("+201127616957", $"Welcome {patient.PatientFirstName} , Your Email for Rosheta Clinic is '{patient.PatientEmail}' and Password is '{capitalizedFirtsChar}' don't share it");
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError("all", error.Description);
                    }
                }
                
            };
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.PatientTypeList = new SelectList(UnitOfWork.PatientTypeRepository.GetAllPatientTypes(), "PatientTypeId", "PatientTypeName");
            return View(UnitOfWork.PatientRepository.GetPatientById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Patient patient)
        {
            ViewBag.PatientTypeList = new SelectList(UnitOfWork.PatientTypeRepository.GetAllPatientTypes(), "PatientTypeId", "PatientTypeName");
            var phones = UnitOfWork.PatientRepository.GetAllPatients().Any(c => c.PatientPhone == patient.PatientPhone);
            patient.PatientId = id; // i Added this line cuz i was having error while trying to update the Patient data please contact me if smth happens Hemaa
            var OldPhone = UnitOfWork.PatientRepository.GetPatientById(patient.PatientId).PatientPhone;
            if (phones && patient.PatientPhone != OldPhone)
            {
                ModelState.AddModelError("PatientPhone", "Already Exist Phone Number");
            }

            if (ModelState.IsValid)
            {
                var oldPass = UnitOfWork.PatientRepository.GetPatientById(patient.PatientId).PatientPassword;
                var same = false;
                if (patient.PatientPassword == null)
                {
                    same=true;
                    patient.PatientPassword = oldPass;
                }
                var user = await UserManager.FindByNameAsync(patient.PatientEmail);

                if (user != null)
                {
                    user.FirstName = patient.PatientFirstName;
                    user.LastName = patient.PatientLastName;
                    user.PhoneNumber = patient.PatientPhone;
                    user.EmailConfirmed = true;
                    if (patient.PatientPassword != null && same == false)
                    {
                        var oldPasswordValidation = UserManager.PasswordHasher.VerifyHashedPassword(user, oldPass, patient.PatientPassword);

                        if (oldPasswordValidation == PasswordVerificationResult.Success)
                        {
                            user.PasswordHash = oldPass;
                        }
                        else
                        {
                            var newPasswordHash = UserManager.PasswordHasher.HashPassword(user, patient.PatientPassword);
                            user.PasswordHash = newPasswordHash;
                        }

                    }
                    else { user.PasswordHash = patient.PatientPassword; }

                    var Result = await UserManager.UpdateAsync(user);

                    if (Result.Succeeded)
                    {
                        patient.PatientPassword = user.PasswordHash;
                        UnitOfWork.PatientRepository.UpdatePatient(id, patient);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in Result.Errors)
                        {
                            ModelState.AddModelError("all", error.Description);
                        }
                    }
                }
            }
            return View();
            
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> EditProfile(int id,PatientEditProfileVM patientEditProfileVM)
        {
            Patient patient = UnitOfWork.PatientRepository.GetPatientById(id);

            if (patient != null)
            {
                if (patientEditProfileVM.PatientGender == 0 || patientEditProfileVM.PatientPhone ==null || patientEditProfileVM.PatientBirthDate == new DateTime())
                {
                    ModelState.AddModelError("Empty", "Complete the required data");
                }
                if (ModelState.IsValid)
                {

                }
                patient.PatientGender = patientEditProfileVM.PatientGender;
                patient.PatientPhone = patientEditProfileVM.PatientPhone;
                patient.PatientBirthDate = patientEditProfileVM.PatientBirthDate;  
            }

            UnitOfWork.PatientRepository.UpdatePatient(id, patient);

            var routeValues = new RouteValueDictionary(new { area = "" });

            return RedirectToAction("Profile","Home", routeValues);

        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> ChangePassword(int id)
        {
            return View();
        }

            [HttpPost]
        public async Task<ActionResult> ChangePassword(int id ,ChangePassword changepassword)
        {
             var patient = UnitOfWork.PatientRepository.GetPatientById(id);

            if ( patient.PatientPhone != changepassword.Phone)
            {
                ModelState.AddModelError("PatientPhone", "Provided Phone number not belong to you");
            }
            if (ModelState.IsValid )
            {
                var user = await UserManager.FindByNameAsync(patient.PatientEmail);

                if (user != null)
                {
                  

                    var newPasswordHash = UserManager.PasswordHasher.HashPassword(user, changepassword.Password);
                    user.PasswordHash = newPasswordHash;
                    var Result = await UserManager.UpdateAsync(user);

                    if (Result.Succeeded)
                    {
                        patient.PatientPassword = user.PasswordHash;
                        UnitOfWork.PatientRepository.UpdatePatient(id, patient);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in Result.Errors)
                        {
                            ModelState.AddModelError("all", error.Description);
                        }
                    }
                }
            }
            return View();
        }
        
        
        
        public ActionResult Delete(int id)
        {
            UnitOfWork.PatientRepository.DeletePatient(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Block(int id)
        {
            await UnitOfWork.PatientRepository.BlockPatient(id);
            return RedirectToAction("Index");
        }
    }
}
