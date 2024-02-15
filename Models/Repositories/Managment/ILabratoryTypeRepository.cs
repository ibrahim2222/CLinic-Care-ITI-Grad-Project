using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface ILabratoryTypeRepository
    {
        public List<LaboratoryType> GetAllLabTypes();
        public LaboratoryType GetLabTypeById(int id);
        public void InsertLabType(LaboratoryType laboratoryType);
        public void UpdateLabType(int id,LaboratoryType laboratoryType);
        public void DeleteLabType(int id );

    }
}
