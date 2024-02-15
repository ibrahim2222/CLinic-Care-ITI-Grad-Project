using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class RadiologyRecord
    {
        public int RadiologyRecordId { get; set; }
        
        [DisplayName("Radio Result")]
        public string RadioResult { get; set; }
        
        [DisplayName("Radio Result Date")]
        public DateTime RadiologyRecordDate { get; set; }
        
        [DisplayName("Appointment")]
        public int AppointmentId { get; set; }

        public virtual Appointment? AppointmentIdNavigation { get; set; }
    }
}
