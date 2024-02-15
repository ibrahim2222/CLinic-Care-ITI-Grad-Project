using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface ILaboratoryRecordRepository
    {
        public List<LaboratoryRecord> GetAllLabRecords();
        public LaboratoryRecord GetLabRecordById(int id);
        public void InsertLabRecord(LaboratoryRecord labRecord);
        public void UpdateLabRecord(int id,LaboratoryRecord labRecord);
        public void DeleteLabRecord(int id);
    }
}
