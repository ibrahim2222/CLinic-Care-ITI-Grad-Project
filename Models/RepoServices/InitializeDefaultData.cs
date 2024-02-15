using Clinc_Care_MVC_Grad_PROJ.Controllers;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices
{
    public class InitializeDefaultData : IInitializeDefaultData
    {
        public RoleManager<IdentityRole> RoleManager { get; }
        public UserManager<AppUser> UserManager { get; }


        public InitializeDefaultData(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
        }

        public async Task Initialize_Data()
        {
            IdentityRole admin = new IdentityRole();
            admin.Name = "Admin";

            IdentityRole doctor = new IdentityRole();
            doctor.Name = "Doctor";
            
            IdentityRole nurse = new IdentityRole();
            nurse.Name = "Nurse";
            
            IdentityRole receptionist = new IdentityRole();
            receptionist.Name = "Receptionist";
            
            IdentityRole examination = new IdentityRole();
            examination.Name = "Examination";

            IdentityRole client = new IdentityRole();
            client.Name = "Client";

            await RoleManager.CreateAsync(admin);
            await RoleManager.CreateAsync(doctor);
            await RoleManager.CreateAsync(nurse);
            await RoleManager.CreateAsync(receptionist);
            await RoleManager.CreateAsync(examination);
            await RoleManager.CreateAsync(client);

            string GeneralPWD = "Admin$123";

            var Admin = new AppUser
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin@roshetaclinic.org",
                Email = "admin@roshetaclinic.org",
                EmailConfirmed = true,
                PasswordHash = GeneralPWD
            };
            await UserManager.CreateAsync(Admin, GeneralPWD);
            await UserManager.AddToRoleAsync(Admin, "Admin");
            
            var Doctor = new AppUser
            {
                FirstName = "Admin",
                LastName = "Doctor",
                UserName = "clinicdoctor@roshetaclinic.org",
                Email = "clinicdoctor@roshetaclinic.org",
                EmailConfirmed = true,
                PasswordHash = GeneralPWD
            };
            await UserManager.CreateAsync(Doctor, GeneralPWD);
            await UserManager.AddToRoleAsync(Doctor, "Doctor");
            
            var Nurse = new AppUser
            {
                FirstName = "Admin",
                LastName = "Nurse",
                UserName = "clinicnurse@roshetaclinic.org",
                Email = "clinicnurse@roshetaclinic.org",
                EmailConfirmed = true,
                PasswordHash = GeneralPWD
            };
            await UserManager.CreateAsync(Nurse, GeneralPWD);
            await UserManager.AddToRoleAsync(Nurse, "Nurse");
            
            var Receptionist = new AppUser
            {
                FirstName = "Admin",
                LastName = "Receptionist",
                UserName = "clinicreceptionist@roshetaclinic.org",
                Email = "clinicreceptionist@roshetaclinic.org",
                EmailConfirmed = true,
                PasswordHash = GeneralPWD
            };
            await UserManager.CreateAsync(Receptionist, GeneralPWD);
            await UserManager.AddToRoleAsync(Receptionist, "Receptionist");
            
            var Examination = new AppUser
            {
                FirstName = "Admin",
                LastName = "Examination",
                UserName = "clinicexamination@roshetaclinic.org",
                Email = "clinicexamination@roshetaclinic.org",
                EmailConfirmed = true,
                PasswordHash = GeneralPWD
            };
            await UserManager.CreateAsync(Examination, GeneralPWD);
            await UserManager.AddToRoleAsync(Examination, "Examination");
            
            string PatientPWD = "Patient$123";

            var Patient = new AppUser
            {
                FirstName = "Test",
                LastName = "Patient",
                UserName = "testpatient@roshetaclinic.com",
                Email = "testpatient@roshetaclinic.com",
                EmailConfirmed = true,
                PasswordHash = PatientPWD
            };
            await UserManager.CreateAsync(Patient, PatientPWD);
            await UserManager.AddToRoleAsync(Patient, "Client");
        }
    }
}
