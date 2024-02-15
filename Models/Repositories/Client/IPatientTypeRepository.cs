using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client
{
    public interface IPatientTypeRepository
    {
        public List<PatientType> GetAllPatientTypes();
        public PatientType GetPatientTypeById(int id);
        public void InsertPatientType(PatientType patientType);
        public void UpdatePatientType(int id, PatientType patientType);
        public void DeletePatientType(int id);
    }
}
