using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface IDoctorSpecializionRepository
    {
        public List<DoctorSpecializion> GetAllDoctorSpecializion();
        public DoctorSpecializion GetDoctorSpecializionById(int id);
        public void InsertDoctorSpecializion(DoctorSpecializion doctorSpecializion);
        public void UpdateDoctorSpecializion(int id, DoctorSpecializion doctorSpecializion);
        public void DeleteDoctorSpecializion(int id);
    }
}
