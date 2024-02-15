using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Microsoft.EntityFrameworkCore;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class PatientVitalRepoService : IPatientVitalRepository
    {
        public ClinicDbContext Ctx { get; }

        public PatientVitalRepoService(ClinicDbContext ctx)
        {
            Ctx = ctx;
        }

        public List<PatientVital> GetAllPatientVitals()
        {
            return Ctx.PatientVitals.ToList();
        }

        public PatientVital GetPatientVitalById(int id)
        {
            var data = Ctx.PatientVitals.Where(V => V.VitalId == id).FirstOrDefault();
            try
            {
                if (data == null)
                {
                    throw new ArgumentException($"Can't Find That EmpDoctorSpecilzation with Id: {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return data;
        }

        public void InsertPatientVital(PatientVital vital)
        {
            if (vital != null)
            {
                try
                {
                    Ctx.Appointments.Where(a => a.AppointmentId == vital.AppointmentId).FirstOrDefault().AppointmentState = AppointmentStates.ready;
                    Ctx.PatientVitals.Add(vital);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void UpdatePatientVital(int id, PatientVital vital)
        {
            var oldvital = GetPatientVitalById(id);
            if (vital != null)
            {
                try
                {
                    oldvital.NurseId = vital.NurseId;
                    oldvital.HeartRate = vital.HeartRate;
                    oldvital.BloodPressure = vital.BloodPressure;
                    oldvital.BloodType = vital.BloodType;
                    oldvital.Temperature = vital.Temperature;
                    oldvital.AppointmentId = vital.AppointmentId;
                    oldvital.Height = vital.Height;
                    oldvital.Weight = vital.Weight;
                    oldvital.PatientComment = vital.PatientComment;
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void DeletePatientVital(int id)
        {
            var vital = GetPatientVitalById(id);
            if (vital != null)
            {
                try
                {
                    Ctx.PatientVitals.Remove(vital);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
