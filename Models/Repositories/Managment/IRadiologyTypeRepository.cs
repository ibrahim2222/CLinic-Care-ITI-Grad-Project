using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface IRadiologyTypeRepository
    {
        public List<RadiologyType> GetAllRadiologyType();
        public RadiologyType GetRadiologyTypeById(int id);
        public void InsertRadiologyType(RadiologyType radiologyType);
        public void UpdateRadiologyType(int id , RadiologyType radiologyType);
        public void DeleteRadiologyType(int id);
    }
}
