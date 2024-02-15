using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface IEmpDoctorSpecializionRepository
    {
        public List<EmpDoctorSpecilzation> GetAllEmpDoctorSpecilzations();
        public EmpDoctorSpecilzation GetEmpDoctorSpecilzationById(int id);
        public void InsertEmpDoctorSpecilzation(EmpDoctorSpecilzation empDoctorSpecilzation);
        public void UpdateEmpDoctorSpecilzation(int id, EmpDoctorSpecilzation empDoctorSpecilzation);
        public void DeleteEmpDoctorSpecilzation(int id);
    }
}
