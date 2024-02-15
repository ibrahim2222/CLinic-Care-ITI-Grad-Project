using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Microsoft.EntityFrameworkCore;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class LaboratoryOrderRepoService : ILaboratoryOrderRepository
    {
        public ClinicDbContext Ctx { get; }

        public LaboratoryOrderRepoService(ClinicDbContext ctx)
        {
            Ctx = ctx;
        }
        public List<LaboratoryOrder> GetAllLaboratoryOrders()
        {
            return Ctx.LaboratoryOrders.ToList();
        }

        public LaboratoryOrder GetLaboratoryOrderById(int id)
        {
            var data = Ctx.LaboratoryOrders.Where(LabOrder => LabOrder.LaboratoryOrderId == id).SingleOrDefault();
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

        public void InsertLaboratoryOrderl(LaboratoryOrder laboratoryOrder)
        {
            if (laboratoryOrder != null)
            {
                try
                {
                    Ctx.LaboratoryOrders.Add(laboratoryOrder);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void UpdateLaboratoryOrder(int id, LaboratoryOrder laboratoryOrder)
        {
            var oldlaboratoryorder = GetLaboratoryOrderById(id);
            if (laboratoryOrder != null)
            {
                try
                {
                    oldlaboratoryorder.LabTypeId = laboratoryOrder.LabTypeId;
                    oldlaboratoryorder.AppointmentId = laboratoryOrder.AppointmentId;
                    oldlaboratoryorder.IsInternal = laboratoryOrder.IsInternal;
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void DeleteLaboratoryOrder(int id)
        {
            var laboratoryOrder = GetLaboratoryOrderById(id);
            if (laboratoryOrder != null)
            {
                try
                {
                    Ctx.LaboratoryOrders.Remove(laboratoryOrder);
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
