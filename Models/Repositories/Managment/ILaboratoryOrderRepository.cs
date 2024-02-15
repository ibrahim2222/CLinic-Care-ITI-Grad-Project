using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface ILaboratoryOrderRepository
    {
        public List<LaboratoryOrder> GetAllLaboratoryOrders();

        public LaboratoryOrder GetLaboratoryOrderById(int id);

        public void InsertLaboratoryOrderl(LaboratoryOrder laboratoryOrder);

        public void UpdateLaboratoryOrder(int id, LaboratoryOrder laboratoryOrder);

        public void DeleteLaboratoryOrder(int id);
    }
}
