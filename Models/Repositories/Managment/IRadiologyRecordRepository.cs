using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface IRadiologyRecordRepository
    {
        public List<RadiologyRecord> GetAllRadiologyRecord();
        public RadiologyRecord GetRadiologyRecordById(int id);
        public void InsertRadiologyRecord(RadiologyRecord record);
        public void UpdateRadiologyRecord(int Id, RadiologyRecord record);
        public void DeleteRadiologyRecord(int Id);
    }
}
