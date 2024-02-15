using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client
{
    public interface IPatientRepository
    {
        public List<Patient> GetAllPatients();

        public Patient GetPatientById(int id);

        public List<Appointment> GetCanceledAppointment(int PatientId);

        public List<Appointment> GetAllAppointment(int PatientId);

        public void InsertPatient(Patient patient);

        public void UpdatePatient(int id, Patient patient);

        public void DeletePatient(int id);

        public Task BlockPatient(int id);

    }
}
