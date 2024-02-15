
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface IMedicineOrderRepository
    {
        public List<MedicineOrder> GetAllMedicineOrders();

        public MedicineOrder GetMedicineOrderById(int id);

        public void InsertMedicineOrder(MedicineOrder medicineOrder);

        public void UpdateMedicineOrder(int id, MedicineOrder medicineOrder);

        public void DeleteMedicineOrder(int id);
    }
}
