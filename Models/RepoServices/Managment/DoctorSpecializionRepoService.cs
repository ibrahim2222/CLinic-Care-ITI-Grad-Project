using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class DoctorSpecializionRepoService : IDoctorSpecializionRepository
    {
        public ClinicDbContext Ctx { get; }
        public DoctorSpecializionRepoService(ClinicDbContext ctx)
        {
            Ctx = ctx;
        }
        public List<DoctorSpecializion> GetAllDoctorSpecializion()
        {
            return Ctx.DoctorSpecializions.ToList();
        }

        public DoctorSpecializion GetDoctorSpecializionById(int id)
        {
            var data = Ctx.DoctorSpecializions.Where(empSpec => empSpec.SpecializationId == id).SingleOrDefault();
            try
            {
                if (data == null)
                {
                    throw new ArgumentException($"Can't Find That DoctorSpecializion with Id: {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return data;
        }

        public void InsertDoctorSpecializion(DoctorSpecializion doctorSpecializion)
        {
            if (doctorSpecializion != null)
            {
                try
                {
                    Ctx.DoctorSpecializions.Add(doctorSpecializion);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void UpdateDoctorSpecializion(int id, DoctorSpecializion employee)
        {
            var updateddoctorSpecializion = GetDoctorSpecializionById(id);

            if (employee != null)
            {
                try
                {
                    updateddoctorSpecializion.SpecializationName = employee.SpecializationName;
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public void DeleteDoctorSpecializion(int id)
        {
            var deleteddoctorSpecializion = GetDoctorSpecializionById(id);
            if (deleteddoctorSpecializion != null)
            {
                try
                {
                    Ctx.DoctorSpecializions.Remove(deleteddoctorSpecializion);
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
