using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class DoctorScheduleRepoService : IDoctorScheduleRepository
    {
        public ClinicDbContext Ctx { get; }

        public DoctorScheduleRepoService(ClinicDbContext ctx)
        {
            Ctx= ctx;
        }
        public List<DoctorSchedule> GetAllDoctorSchedule()
        {
            return Ctx.DoctorSchedules.ToList();
        }

        public DoctorSchedule GetDoctorDoctorScheduleById(int id)
        {
            var data = Ctx.DoctorSchedules.Where(Sch => Sch.ScheduleId == id).SingleOrDefault();
            try
            {
                if (data == null)
                {
                    throw new ArgumentException($"Can't Find That Doctor Schedule with Id: {id}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return data;
        }

        public void InsertDoctorDoctorSchedule(DoctorSchedule doctorSchedule)
        {
            if (doctorSchedule != null)
            {
                try
                {
                    Ctx.DoctorSchedules.Add(doctorSchedule);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void UpdateDoctorSchedule(int id, DoctorSchedule doctorSchedule)
        {
            var updatedoctorSchedule = GetDoctorDoctorScheduleById(id);
            if (doctorSchedule != null)
            {
                try
                {
                    updatedoctorSchedule.DoctorId = doctorSchedule.DoctorId;
                    updatedoctorSchedule.DaysOfWeek = doctorSchedule.DaysOfWeek;

                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public void DeleteDoctorSchedule(int id)
        {
            var deletedSchedule = GetDoctorDoctorScheduleById(id);
            if (deletedSchedule != null)
            {
                try
                {
                    Ctx.DoctorSchedules.Remove(deletedSchedule);
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
