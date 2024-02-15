using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Clinc_Care_MVC_Grad_PROJ.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Controllers
{
    [Authorize(Roles = "Admin,Doctor,Nurse,Receptionist,Examination")]

    [Area("Managment")]
    public class EmployeeController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }
        public UserManager<AppUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

        public EmployeeController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            UnitOfWork = unitOfWork;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public ActionResult Index()
        {
            return View("Index", UnitOfWork.EmployeeRepository.GetAllEmployees());
        }
        public ActionResult Details(int id)
        {
            return View("Details", UnitOfWork.EmployeeRepository.GetEmployeeById(id));
        }
        public ActionResult Create()
        {
            ViewBag.EmpTypeList = new SelectList(UnitOfWork.EmployeeTypeRepository.GetAllEmployeeType(), "EmployeeTypeId", "TypeName");
            ViewBag.DoctorSpecializionList = new SelectList(UnitOfWork.DoctorSpecializionRepository.GetAllDoctorSpecializion(), "SpecializationId", "SpecializationName");
            ViewBag.RoleList = new SelectList(RoleManager.Roles.Select(c => c.Name).ToList(), "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmpDoctorVM employee)
        {
            ViewBag.EmpTypeList = new SelectList(UnitOfWork.EmployeeTypeRepository.GetAllEmployeeType(), "EmployeeTypeId", "TypeName");
            ViewBag.DoctorSpecializionList = new SelectList(UnitOfWork.DoctorSpecializionRepository.GetAllDoctorSpecializion(), "SpecializationId", "SpecializationName");
            ViewBag.RoleList = new SelectList(RoleManager.Roles.Select(c => c.Name).ToList(), "Name");
            if (employee.EmpPhone == null)
            {
                ModelState.AddModelError("EmpPhone", "You must enter phone number");
            }
            else
            {
                var phones = UnitOfWork.EmployeeRepository.GetAllEmployees().Any(c => c.EmpPhone == employee.EmpPhone);
                if (phones)
                {
                    ModelState.AddModelError("EmpPhone", "Already Exist Phone Number");
                }
            }

            var NationalId = UnitOfWork.EmployeeRepository.GetAllEmployees().Any(c => c.EmpNationalId == employee.EmpNationalId);
            if (NationalId)
            {
                ModelState.AddModelError("EmpNationalId", "Already Exist National Id");
            }

            if (employee.EmpPassword == null)
            {
                ModelState.AddModelError("EmpPassword", "you must enter password");
            }

            //if (employee.EmployeeTypeIdNavigation.TypeName.ToLower() == "doctor")
            //{
            //    employee.DoctorSpecializion = 0;
            //}
            var Doctortype = UnitOfWork.EmployeeTypeRepository.GetEmployeeTypeById(employee.EmployeeTypeId).TypeName.ToLower() == "doctor";
            if (Doctortype && employee.DoctorSpecializion == 0)
            {
                ModelState.AddModelError("DoctorSpecializion", "you must Select Doctor Specializion");
            }


            var data = UnitOfWork.EmpDoctorSpecializionRepository.GetAllEmpDoctorSpecilzations().Any(c => c.DoctorSpecilzationId == employee.DoctorSpecializion);

            if (data)
            {
                ModelState.AddModelError("DoctorSpecializion", "this Specializion Already Has A doctor");
            }

            if (ModelState.IsValid)
            {
                Random random = new Random();
                int randomNum = random.Next(1, 201);
                long newNum = Convert.ToInt64(2 + employee.EmpPhone) / 250000 * 2;
                var domain = $"{employee.EmpFirstName}_{newNum}@roshetaclinic.org";

                AppUser user = new AppUser()
                {
                    FirstName = employee.EmpFirstName,
                    LastName = employee.EmpLastName,
                    UserName = domain,
                    Email = domain,
                    PhoneNumber = employee.EmpPhone,
                    PasswordHash = employee.EmpPassword,
                    EmailConfirmed = true,
                };
                
                var Result =  await UserManager.CreateAsync(user, employee.EmpPassword);

                if (Result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, employee.RoleName);
                    employee.EmpEmail = user.Email;
                    UnitOfWork.EmployeeRepository.InsertEmployee(employee);

                    if (employee.DoctorSpecializion != 0)
                    {
                        EmpDoctorSpecilzation empDoctorSpecilzation = new EmpDoctorSpecilzation()
                        {
                            DoctorSpecilzationId = employee.DoctorSpecializion,
                            DoctorId = employee.EmpId
                        };
                        UnitOfWork.EmpDoctorSpecializionRepository.InsertEmpDoctorSpecilzation(empDoctorSpecilzation);
                    };
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError("all", error.Description);
                    }
                }
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.EmpTypeList = new SelectList(UnitOfWork.EmployeeTypeRepository.GetAllEmployeeType(), "EmployeeTypeId", "TypeName");
            ViewBag.DoctorSpecializionList = new SelectList(UnitOfWork.DoctorSpecializionRepository.GetAllDoctorSpecializion(), "SpecializationId", "SpecializationName");
            ViewBag.RoleList = new SelectList(RoleManager.Roles.Select(c => c.Name).ToList(), "Name");
            return View(UnitOfWork.EmployeeRepository.GetEmployeeById(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Employee employee)
        {
            ViewBag.EmpTypeList = new SelectList(UnitOfWork.EmployeeTypeRepository.GetAllEmployeeType(), "EmployeeTypeId", "TypeName");
            ViewBag.DoctorSpecializionList = new SelectList(UnitOfWork.DoctorSpecializionRepository.GetAllDoctorSpecializion(), "SpecializationId", "SpecializationName");
            ViewBag.RoleList = new SelectList(RoleManager.Roles.Select(c => c.Name).ToList(), "Name");
            if (employee.EmpPhone == null)
            {
                ModelState.AddModelError("EmpPhone", "You must enter phone number");
            }
            else
            {
                var phones = UnitOfWork.EmployeeRepository.GetAllEmployees().Any(c => c.EmpPhone == employee.EmpPhone);
                if (phones)
                {
                    ModelState.AddModelError("EmpPhone", "Already Exist Phone Number");
                }
            }

            var NationalId = UnitOfWork.EmployeeRepository.GetAllEmployees().Any(c => c.EmpNationalId == employee.EmpNationalId);
            if (NationalId)
            {
                ModelState.AddModelError("EmpNationalId", "Already Exist National Id");
            }
            if (ModelState.IsValid)
            {
                var oldPass = UnitOfWork.EmployeeRepository.GetEmployeeById(employee.EmpId).EmpPassword;
                if (employee.EmpPassword == null)
                {
                    employee.EmpPassword = oldPass;
                }

                Random random = new Random();
                int randomNum = random.Next(1, 201);
                long newNum = Convert.ToInt64(2 + employee.EmpPhone) / 250000 * 2;
                var domain = $"{employee.EmpFirstName}_{newNum}@roshetaclinic.org";

                var user = await UserManager.FindByNameAsync(employee.EmpEmail);

                if (user != null)
                {
                    user.FirstName = employee.EmpFirstName;
                    user.LastName = employee.EmpLastName;
                    user.UserName = domain;
                    user.Email = domain;
                    user.PhoneNumber = employee.EmpPhone;

                    if (employee.EmpPassword != null)
                    {
                        var newPasswordHash = UserManager.PasswordHasher.HashPassword(user, employee.EmpPassword);
                        user.PasswordHash = newPasswordHash;
                    }
                    var Result = await UserManager.UpdateAsync(user);

                    if (Result.Succeeded)
                    {
                        employee.EmpEmail = domain;
                        UnitOfWork.EmployeeRepository.UpdateEmployee(id, employee);
                        return RedirectToAction(nameof(Index));
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


        public ActionResult DoctorReadyAppoitments()
        {
            return View("DoctorReadyAppoitments",UnitOfWork.AppointmentRepository.GetAllAppointments());
        }
        public ActionResult DetailedPatientAppointment(int id)
        {
            ViewBag.LabTests = UnitOfWork.LabratoryTypeRepository.GetAllLabTypes();
            ViewBag.Radiologies = UnitOfWork.RadiologyTypeRepository.GetAllRadiologyType();
            ViewBag.Medicines = new SelectList(UnitOfWork.MedicineRepository.GetAllMedicines().Where(m=>!m.MedicineOrders.Select(s=>s.AppointmentId).Contains(id)), "MedicineId", "MedicineName");
            ViewBag.Dosages = new SelectList(UnitOfWork.DosageRepository.GetAllDosages().ToList(), "DosageId", "DosageName");
            return View("DetailedPatientAppointment", UnitOfWork.AppointmentRepository.GetAppointmentById(id));
        }

        public async Task <ActionResult> Delete(int id)
        {
            await UnitOfWork.EmployeeRepository.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
