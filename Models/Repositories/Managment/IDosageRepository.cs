using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface IDosageRepository
    {
        public List<Dosage> GetAllDosages();

        public Dosage GetDosageById(int id);

        public void InsertDosage(Dosage dosage);

        public void UpdateDosage(int id, Dosage dosage);

        public void DeleteDosage(int id);
    }
}
