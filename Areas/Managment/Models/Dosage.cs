using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class Dosage
    {
        public Dosage()
        {
            MedicineOrders = new HashSet<MedicineOrder>();
        }

        public int DosageId { get; set; }

        [DisplayName("Dosage Name")]
        public string DosageName { get; set; }

        public virtual ICollection<MedicineOrder> MedicineOrders { get; set; }
    }
}
