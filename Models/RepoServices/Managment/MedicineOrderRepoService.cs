using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Microsoft.EntityFrameworkCore;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class MedicineOrderRepoService : IMedicineOrderRepository
    {
        public ClinicDbContext Ctx { get; }

        public MedicineOrderRepoService(ClinicDbContext ctx)
        {
            Ctx = ctx;
        }

        public List<MedicineOrder> GetAllMedicineOrders()
        {
            return Ctx.MedicineOrders.ToList();
        }

        public MedicineOrder GetMedicineOrderById(int id)
        {
            var data = Ctx.MedicineOrders.Where(M => M.MedicineOrderId == id).SingleOrDefault();
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

        public void InsertMedicineOrder(MedicineOrder medicineOrder)
        {
            if (medicineOrder != null)
            {
                try
                {
                    Ctx.MedicineOrders.Add(medicineOrder);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void UpdateMedicineOrder(int id, MedicineOrder medicineOrder)
        {
            var oldmedicineOrder = GetMedicineOrderById(id);
            if (medicineOrder != null)
            {
                try
                {
                    oldmedicineOrder.Instructions = medicineOrder.Instructions;
                    oldmedicineOrder.AppointmentId = medicineOrder.AppointmentId;
                    oldmedicineOrder.DosageId= medicineOrder.DosageId;
                    oldmedicineOrder.MedicineId= medicineOrder.MedicineId;
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void DeleteMedicineOrder(int id)
        {
            var medicineOrder = GetMedicineOrderById(id);
            if (medicineOrder != null)
            {
                try
                {
                    Ctx.MedicineOrders.Remove(medicineOrder);
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
