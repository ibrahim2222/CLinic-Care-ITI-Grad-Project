using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client
{
    public interface IAppointmentTypeRepository
    {
        public List<AppointmentType> GetAllAppointmentTypes();

        public AppointmentType GetAppointmentTypeById(int id);

        public void InsertAppointmentType(AppointmentType appointmentType);

        public void UpdateAppointmentType(int id, AppointmentType appointmentType);

        public void DeleteAppointmentType(int id);
    }
}
