using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Microsoft.EntityFrameworkCore;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class RadiologyOrderRepoService : IRadiologyOrderRepository
    {
        public ClinicDbContext Ctx { get; }

        public RadiologyOrderRepoService(ClinicDbContext ctx)
        {
            Ctx = ctx;
        }

        public List<RadiologyOrder> GetAllRadiologyOrders()
        {
            return Ctx.RadiologyOrders.ToList();
        }

        public RadiologyOrder GetRadiologyOrderById(int id)
        {
            var data = Ctx.RadiologyOrders.Where(radiologyOrder => radiologyOrder.RadiologyOrderId == id).SingleOrDefault();
            try
            {
                if (data == null)
                {
                    throw new ArgumentException($"Can't Find That EmpDoctorSpecilzation with Id: {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return data;
        }

        public void InsertRadiologyOrder(RadiologyOrder radiologyOrder)
        {
            if (radiologyOrder != null)
            {
                try
                {
                    Ctx.RadiologyOrders.Add(radiologyOrder);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void UpdateRadiologyOrder(int id, RadiologyOrder radiologyOrder)
        {
            var oldradiologyOrder = GetRadiologyOrderById(id);
            if (radiologyOrder != null)
            {
                try
                {
                    oldradiologyOrder.AppointmentId = radiologyOrder.AppointmentId;
                    oldradiologyOrder.RadiologyTypeId= radiologyOrder.RadiologyTypeId;
                    oldradiologyOrder.IsInternal = radiologyOrder.IsInternal;
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void DeleteRadiologyOrder(int id)
        {
            var radiologyOrder = GetRadiologyOrderById(id);
            if (radiologyOrder != null)
            {
                try
                {
                    Ctx.RadiologyOrders.Remove(radiologyOrder);
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
