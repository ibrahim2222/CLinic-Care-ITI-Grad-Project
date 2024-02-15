using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client;
using Microsoft.EntityFrameworkCore;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Client
{
    public class AppointmentTypeRepoService : IAppointmentTypeRepository
    {
        public ClinicDbContext Ctx { get; }
        public AppointmentTypeRepoService(ClinicDbContext ctx)
        {
            Ctx = ctx;
        }
        public List<AppointmentType> GetAllAppointmentTypes()
        {
            return Ctx.AppointmentTypes.ToList();
        }
        public AppointmentType GetAppointmentTypeById(int id)
        {
            var data = Ctx.AppointmentTypes.Where(atype => atype.AppointmentTypeId == id).SingleOrDefault();
            try
            {
                if (data == null)
                {
                    throw new ArgumentException($"Can't Find That AppointmentType with Id: {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return data ;
        }
        public void InsertAppointmentType(AppointmentType appointmentType)
        {
            if (appointmentType != null)
            {
                try
                {
                    Ctx.AppointmentTypes.Add(appointmentType);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public void UpdateAppointmentType(int id, AppointmentType appointmentType)
        {
            var updatedAppointmentType = GetAppointmentTypeById(id);
            if (appointmentType != null)
            {
                try
                {
                    updatedAppointmentType.TypeName = appointmentType.TypeName;

                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public void DeleteAppointmentType(int id)
        {
            var deletedAppointmentType = GetAppointmentTypeById(id);
            if (deletedAppointmentType != null)
            {
                try
                {
                    Ctx.AppointmentTypes.Remove(deletedAppointmentType);
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
