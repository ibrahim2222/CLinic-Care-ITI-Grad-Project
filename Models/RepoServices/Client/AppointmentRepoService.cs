using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Client
{
    public class AppointmentRepoService : IAppointmentRepository
    {
        public ClinicDbContext Ctx { get; }
        public IPatientRepository PatientRepository { get; }

        public AppointmentRepoService(ClinicDbContext ctx, IPatientRepository patientRepository)
        {
            Ctx = ctx;
            PatientRepository = patientRepository;
        }
        public List<Appointment> GetAllAppointments()
        {
            return Ctx.Appointments.ToList();
        }
        public Appointment GetAppointmentById(int id)
        {
            var data = Ctx.Appointments.Where(appo => appo.AppointmentId == id).SingleOrDefault();
            try
            {
                if (data == null)
                {
                    throw new ArgumentException($"Can't Find That Appointment with Id: {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return data;
        }
        public void InsertAppointment(Appointment appointment)
        {
            if (appointment != null)
            {
                try
                {
                    Ctx.Appointments.Add(appointment);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public void UpdateAppointment(int id, Appointment appointment)
        {
            var updatedAppointment = GetAppointmentById(id);
            if (appointment != null)
            {
                try
                {
                    updatedAppointment.AppointmentTypeId = appointment.AppointmentTypeId;
                    updatedAppointment.PaymentMathodId = appointment.PaymentMathodId;
                    updatedAppointment.PatientId = appointment.PatientId;
                    updatedAppointment.DoctorId = appointment.DoctorId;
                    updatedAppointment.ExaminationComment = appointment.ExaminationComment;
                    updatedAppointment.AppointmentDate = appointment.AppointmentDate;
                    //updatedAppointment.CreatedAppointmentDate = updatedAppointment.CreatedAppointmentDate;
                    updatedAppointment.UpdatedAppointmentDate = DateTime.Now;
                    updatedAppointment.IsCanceled = appointment.IsCanceled;
                    updatedAppointment.IsPaid = appointment.IsPaid;
                    updatedAppointment.AppointmentState = appointment.AppointmentState;

                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public void DeleteAppointment(int id, bool isPayment)
        {
            var deletedAppointment = GetAppointmentById(id);
            var patientAppointnebts = Ctx.Appointments.Where(c => c.PatientId == deletedAppointment.PatientId);
            if (deletedAppointment != null)
            {
                try
                {
                    if (!isPayment)
                    {
                        if (deletedAppointment.IsCanceled == true)
                        {
                            deletedAppointment.IsCanceled = false;
                            deletedAppointment.UpdatedAppointmentDate = DateTime.Now;
                            deletedAppointment.AppointmentState = AppointmentStates.reserverd;
                        }
                        else
                        {
                            var counOfCanceledAppointments = patientAppointnebts.OrderByDescending(c => c.CreatedAppointmentDate)
                                                                                .Skip(1)
                                                                                .Take(4)
                                                                                .Select(c => c.IsCanceled)
                                                                                .ToList();

                            if (!counOfCanceledAppointments.Contains(false) && counOfCanceledAppointments.Count >=5)
                            {
                                PatientRepository.BlockPatient(deletedAppointment.PatientId);
                            }

                            deletedAppointment.IsCanceled = true;
                            deletedAppointment.UpdatedAppointmentDate = DateTime.Now;
                            deletedAppointment.AppointmentState = AppointmentStates.canceled;
                        }
                    }
                    else
                    {
                        Ctx.Appointments.Remove(deletedAppointment);
                    }
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
