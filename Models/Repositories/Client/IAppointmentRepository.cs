using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client
{
    public interface IAppointmentRepository
    {
        public List<Appointment> GetAllAppointments();

        public Appointment GetAppointmentById(int id);

        public void InsertAppointment(Appointment appointment);

        public void UpdateAppointment(int id, Appointment appointment);

        public void DeleteAppointment(int id,bool isPayment);
    }
}
