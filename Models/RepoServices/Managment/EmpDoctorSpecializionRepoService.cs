using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class EmpDoctorSpecializionRepoService: IEmpDoctorSpecializionRepository
    {
        public ClinicDbContext Ctx { get; }
        public EmpDoctorSpecializionRepoService(ClinicDbContext ctx)
        {
            Ctx = ctx;
        }

        public List<EmpDoctorSpecilzation> GetAllEmpDoctorSpecilzations()
        {
            return Ctx.EmpDoctorSpecilzations.ToList();
        }

        public EmpDoctorSpecilzation GetEmpDoctorSpecilzationById(int id)
        {
            var data = Ctx.EmpDoctorSpecilzations.Where(c => c.EmpDoctorSpecilzationId == id).SingleOrDefault();

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

        public void InsertEmpDoctorSpecilzation(EmpDoctorSpecilzation empDoctorSpecilzation)
        {
            if (empDoctorSpecilzation != null)
            {
                try
                {
                    Ctx.EmpDoctorSpecilzations.Add(empDoctorSpecilzation);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void UpdateEmpDoctorSpecilzation(int id, EmpDoctorSpecilzation empDoctorSpecilzation)
        {
            var updatedEmpDoctorSpecilzation = GetEmpDoctorSpecilzationById(id);
            if (empDoctorSpecilzation != null)
            {
                try
                {
                    updatedEmpDoctorSpecilzation.DoctorId = empDoctorSpecilzation.DoctorId;
                    updatedEmpDoctorSpecilzation.DoctorSpecilzationId = empDoctorSpecilzation.DoctorSpecilzationId;

                    Ctx.SaveChanges();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void DeleteEmpDoctorSpecilzation(int id)
        {
            var deletedEmpDoctorSpecilzation = GetEmpDoctorSpecilzationById(id);

            if (deletedEmpDoctorSpecilzation != null)
            {
                try
                {
                    Ctx.EmpDoctorSpecilzations.Remove(deletedEmpDoctorSpecilzation);
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
