using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client
{
    public interface IFeeRepository
    {
        public List<Fee> GetAllFees();
        public Fee GetFeeById(int id);
        public void InsertFee(Fee fees);
        public void UpdateFee(int id, Fee fees);
        public void DeleteFee(int id);
    }
}
