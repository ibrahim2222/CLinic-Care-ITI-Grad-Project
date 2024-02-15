using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Client
{
    public class PatientTypeRepoService : IPatientTypeRepository
    {
        public ClinicDbContext Ctx { get; }
        public PatientTypeRepoService(ClinicDbContext ctx)
        {
            Ctx = ctx;
        }
        
        public List<PatientType> GetAllPatientTypes()
        {
            return Ctx.PatientTypes.ToList();
        }

        public PatientType GetPatientTypeById(int id)
        {
            var data = Ctx.PatientTypes.Where(emp => emp.PatientTypeId == id).SingleOrDefault();

            try
            {
                if (data == null)
                {
                    throw new ArgumentException($"Can't Find That Patient Type with Id: {id}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return data;
        }

        public void InsertPatientType(PatientType patientType)
        {
            if (patientType != null)
            {
                try
                {
                    Ctx.PatientTypes.Add(patientType);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void UpdatePatientType(int id, PatientType patientType)
        {
            var updatedPatientType = GetPatientTypeById(id);

            if (patientType != null)
            {
                try
                {
                    updatedPatientType.PatientTypeName = patientType.PatientTypeName;
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void DeletePatientType(int id)
        {
            var deletedPatientType = GetPatientTypeById(id);
            if (deletedPatientType != null)
            {
                try
                {
                    Ctx.PatientTypes.Remove(deletedPatientType);
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
