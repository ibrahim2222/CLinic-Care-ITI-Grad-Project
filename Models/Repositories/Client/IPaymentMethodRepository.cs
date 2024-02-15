using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client
{
    public interface IPaymentMethodRepository
    {
        public List<PaymentMethod> GetAllPaymentMethods();

        public PaymentMethod GetPaymentMethodById(int id);

        public void InsertPaymentMethod(PaymentMethod paymentMethod);

        public void UpdatePaymentMethod(int id, PaymentMethod paymentMethod);

        public void DeletePaymentMethod(int id);
    }
}
