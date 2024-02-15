using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Client
{
    public class PatientRepoService : IPatientRepository
    {
        public ClinicDbContext Ctx { get; }
        public UserManager<AppUser> UserManager { get; }

        public PatientRepoService(ClinicDbContext ctx, UserManager<AppUser> userManager)
        {
            Ctx = ctx;
            UserManager = userManager;

        }
        public List<Patient> GetAllPatients()
        {
            return Ctx.Patients.ToList();
        }
        public Patient GetPatientById(int id)
        {
            var data = Ctx.Patients.Where(pat => pat.PatientId == id).SingleOrDefault();
            try
            {
                if (data == null)
                {
                    throw new ArgumentException($"Can't Find That Patient with Id: {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return data;
        }


        public void InsertPatient(Patient patient)
        {
            if (patient != null)
            {
                try
                {
                    Ctx.Patients.Add(patient);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public void UpdatePatient(int id, Patient patient)
        {
            var updatedPatient = GetPatientById(id);
            if (patient != null)
            {
                try
                {
                    updatedPatient.PatientFirstName = patient.PatientFirstName;
                    updatedPatient.PatientLastName = patient.PatientLastName;
                    updatedPatient.PatientBirthDate = patient.PatientBirthDate;
                    updatedPatient.PatientGender = patient.PatientGender;
                    updatedPatient.PatientPhone = patient.PatientPhone;
                    updatedPatient.PatientPassword = patient.PatientPassword;
                    updatedPatient.PatientEmail = patient.PatientEmail;
                    updatedPatient.PatientTypeId = patient.PatientTypeId;
                    

                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public void DeletePatient(int id)
        {
            var deletedPatient = GetPatientById(id);
            var data = Ctx.PatientTypes.Where(c => c.PatientTypeId == deletedPatient.PatientTypeId).Select(c => c.PatientTypeName).SingleOrDefault().ToLower();
            if (deletedPatient != null)
            {
                try
                {
                    if(data == "deleted")
                    {
                        deletedPatient.PatientTypeId = Ctx.PatientTypes
                            .Where(c => c.PatientTypeName.ToLower() == "online")
                            .Select(c => c.PatientTypeId).SingleOrDefault();
                    }
                    else
                    {
                        deletedPatient.PatientTypeId = Ctx.PatientTypes
                            .Where(c => c.PatientTypeName.ToLower() == "deleted")
                            .Select(c => c.PatientTypeId).SingleOrDefault();
                    }
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        
        public async Task BlockPatient(int id)
        {
            var blockedPatient = GetPatientById(id);
            AppUser userFromDB = await UserManager.FindByEmailAsync(blockedPatient.PatientEmail);
            var data = Ctx.PatientTypes.Where(c => c.PatientTypeId == blockedPatient.PatientTypeId).Select(c => c.PatientTypeName).SingleOrDefault().ToLower();
            if (blockedPatient != null)
            {
                try
                {
                    if (data == "blocked")
                    {
                        blockedPatient.PatientTypeId = Ctx.PatientTypes
                            .Where(c => c.PatientTypeName.ToLower() == "online")
                            .Select(c => c.PatientTypeId).SingleOrDefault();
                        userFromDB.EmailConfirmed = true;
                    }
                    else
                    {
                        blockedPatient.PatientTypeId = Ctx.PatientTypes
                            .Where(c => c.PatientTypeName.ToLower() == "blocked")
                            .Select(c => c.PatientTypeId).SingleOrDefault();
                        userFromDB.EmailConfirmed = false;
                    }
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public List<Appointment> GetCanceledAppointment(int PatientId)
        {
            var canceledAppointments = Ctx.Appointments.Where(c => c.IsCanceled == true && c.PatientId == PatientId).ToList();

            return canceledAppointments;
        }
        
        public List<Appointment> GetAllAppointment(int PatientId)
        {
            var Appointments = Ctx.Appointments.Where(c => c.PatientId == PatientId).ToList();

            return Appointments;
        }
    }
}
