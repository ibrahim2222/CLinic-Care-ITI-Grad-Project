using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Client
{
    public class FeeRepoService : IFeeRepository
    {
        public ClinicDbContext Ctx { get; }
        public FeeRepoService(ClinicDbContext ctx)
        {
            Ctx = ctx;
        }
        
        public List<Fee> GetAllFees()
        {
            return Ctx.Fees.ToList();
        }

        public Fee GetFeeById(int id)
        {
            var data = Ctx.Fees.Where(fee => fee.FeeId == id).SingleOrDefault();

            try
            {
                if (data == null)
                {
                    throw new ArgumentException($"Can't Find That Fee with Id: {id}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return data;
        }

        public void InsertFee(Fee fee)
        {
            if (fee != null)
            {
                try
                {
                    Ctx.Fees.Add(fee);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void UpdateFee(int id, Fee fee)
        {
            var updatedFee = GetFeeById(id);

            if (fee != null)
            {
                try
                {
                    updatedFee.FeeName = fee.FeeName;
                    updatedFee.FeeAmount = fee.FeeAmount;
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void DeleteFee(int id)
        {
            var DeletedFee = GetFeeById(id);
            if (DeletedFee != null)
            {
                try
                {
                    Ctx.Fees.Remove(DeletedFee);
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
