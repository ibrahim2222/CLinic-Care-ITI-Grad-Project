using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client;
using Microsoft.EntityFrameworkCore;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Client
{
    public class PaymentMethodRepoService : IPaymentMethodRepository
    {
        public ClinicDbContext Ctx { get; }
        public PaymentMethodRepoService(ClinicDbContext ctx)
        {
            Ctx = ctx;
        }
        public List<PaymentMethod> GetAllPaymentMethods()
        {
            return Ctx.PaymentMethods.ToList();
        }
        public PaymentMethod GetPaymentMethodById(int id)
        {
            var data = Ctx.PaymentMethods.Where(pay => pay.PaymentMethodId == id).SingleOrDefault();
            try
            {
                if (id == null)
                {
                    throw new ArgumentException($"Can't Find That PaymentMethod with Id: {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return data;
        }
        public void InsertPaymentMethod(PaymentMethod paymentMethod)
        {
            if (paymentMethod != null)
            {
                try
                {
                    Ctx.PaymentMethods.Add(paymentMethod);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public void UpdatePaymentMethod(int id, PaymentMethod paymentMethod)
        {
            var updatedPaymentMethod = GetPaymentMethodById(id);
            if (paymentMethod != null)
            {
                try
                {
                    updatedPaymentMethod.PaymentMethodName = paymentMethod.PaymentMethodName;

                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public void DeletePaymentMethod(int id)
        {
            var deletedPaymentMethod = GetPaymentMethodById(id);
            if (deletedPaymentMethod != null)
            {
                try
                {
                    Ctx.PaymentMethods.Remove(deletedPaymentMethod);
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
