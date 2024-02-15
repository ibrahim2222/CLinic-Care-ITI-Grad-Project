using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface IMedicineRepository
    {
        public List<Medicine> GetAllMedicines();

        public Medicine GetMedicineById(int id);

        public void InsertMedicine(Medicine medicine);

        public void UpdateMedicine(int id, Medicine medicine);

        public void DeleteMedicine(int id);
    }
}
