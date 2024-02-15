using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class Medicine
    {
        public Medicine()
        {
            MedicineOrders = new HashSet<MedicineOrder>();
        }

        public int MedicineId { get; set; }
        
        [DisplayName("Medicine Name")]
        public string MedicineName { get; set; }
        
        [DisplayName("Description")]
        public string MedicineDescription { get; set; }

        public virtual ICollection<MedicineOrder> MedicineOrders { get; set; }  
    }
}
