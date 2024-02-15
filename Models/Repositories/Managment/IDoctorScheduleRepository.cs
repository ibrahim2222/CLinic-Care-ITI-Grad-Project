using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface IDoctorScheduleRepository
    {
        public List<DoctorSchedule> GetAllDoctorSchedule();
        public DoctorSchedule GetDoctorDoctorScheduleById(int id);
        public void InsertDoctorDoctorSchedule(DoctorSchedule doctorSchedule);
        public void UpdateDoctorSchedule(int id, DoctorSchedule doctorSchedule);
        public void DeleteDoctorSchedule(int id);
    }
}
