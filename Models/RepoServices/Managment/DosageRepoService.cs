using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class DosageRepoService : IDosageRepository
    {
        public ClinicDbContext Ctx { get; }

        public DosageRepoService(ClinicDbContext ctx)
        {
            Ctx = ctx;
        }

        public List<Dosage> GetAllDosages()
        {
            return Ctx.Dosages.ToList();
        }

        public Dosage GetDosageById(int id)
        {
            var data = Ctx.Dosages.Where(M => M.DosageId == id).SingleOrDefault();
            try
            {
                if (data == null)
                {
                    throw new ArgumentException($"Can't Find That Dosage with Id: {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return data;
        }

        public void InsertDosage(Dosage dosage)
        {
            if (dosage != null)
            {
                try
                {
                    Ctx.Dosages.Add(dosage);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void UpdateDosage(int id, Dosage dosage)
        {
            var oldDosage = GetDosageById(id);
            if (dosage != null)
            {
                try
                {
                    oldDosage.DosageName = dosage.DosageName;
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void DeleteDosage(int id)
        {
            var dosage = GetDosageById(id);
            if (dosage != null)
            {
                try
                {
                    Ctx.Dosages.Remove(dosage);
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
