using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class MedicineOrder
    {
        public int MedicineOrderId { get; set; } 
        
        [DisplayName("Medicine")]
        public int MedicineId { get; set; } 
        
        [DisplayName("Dosage")]
        public int DosageId { get; set; } 
        
        [DisplayName("Appointment")]
        public int AppointmentId { get; set; }

        [DisplayName("Instructions")]
        public string Instructions { get; set; }

        public virtual Medicine? MedicineIdNavigation { get; set; }
        public virtual Dosage? DosageIdNavigation { get; set; }
        public virtual Appointment? AppointmentIdNavigation { get; set; }
    }
}
