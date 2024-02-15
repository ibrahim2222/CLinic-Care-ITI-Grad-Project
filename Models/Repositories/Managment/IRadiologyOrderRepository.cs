
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface IRadiologyOrderRepository
    {
        public List<RadiologyOrder> GetAllRadiologyOrders();

        public RadiologyOrder GetRadiologyOrderById(int id);

        public void InsertRadiologyOrder(RadiologyOrder radiologyOrder);

        public void UpdateRadiologyOrder(int id, RadiologyOrder radiologyOrder);

        public void DeleteRadiologyOrder(int id);
    }
}
