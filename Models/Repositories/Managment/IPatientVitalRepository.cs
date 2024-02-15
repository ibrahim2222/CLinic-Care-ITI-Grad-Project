
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface IPatientVitalRepository
    {
        public List<PatientVital> GetAllPatientVitals();
        public PatientVital GetPatientVitalById(int id);
        public void InsertPatientVital(PatientVital employee);
        public void UpdatePatientVital(int id, PatientVital employee);
        public void DeletePatientVital(int id);
    }
}
