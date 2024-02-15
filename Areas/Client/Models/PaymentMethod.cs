using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models
{
    public class PaymentMethod
    {
        public PaymentMethod()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int PaymentMethodId { get; set; }
        
        [DisplayName("Payment Method Name")]
        public string PaymentMethodName { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
