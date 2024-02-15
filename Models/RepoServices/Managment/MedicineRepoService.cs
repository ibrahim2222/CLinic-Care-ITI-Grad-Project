using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class MedicineRepoService : IMedicineRepository
    {
        public ClinicDbContext Ctx { get; }

        public MedicineRepoService(ClinicDbContext ctx)
        {
            Ctx = ctx;
        }

        public List<Medicine> GetAllMedicines()
        {
            return Ctx.Medicines.ToList();
        }

        public Medicine GetMedicineById(int id)
        {
            var data = Ctx.Medicines.Where(c => c.MedicineId == id).SingleOrDefault();

            try
            {
                if (data == null)
                {
                    throw new ArgumentException($"Can't Find That Medicine with Id: {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return data;
        }

        public void InsertMedicine(Medicine medicine)
        {
            if (medicine != null)
            {
                try
                {
                    Ctx.Medicines.Add(medicine);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void UpdateMedicine(int id, Medicine medicine)
        {
            var oldMedicine = GetMedicineById(id);
            if (medicine != null)
            {
                try
                {
                    oldMedicine.MedicineName = medicine.MedicineName;
                    oldMedicine.MedicineDescription = medicine.MedicineDescription;
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void DeleteMedicine(int id)
        {
            var medicine = GetMedicineById(id);
            if (medicine != null)
            {
                try
                {
                    Ctx.Medicines.Remove(medicine);
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
