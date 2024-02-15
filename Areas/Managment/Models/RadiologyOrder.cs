using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class RadiologyOrder
    {
        public int RadiologyOrderId { get; set; }
        
        [DisplayName("Appointment")]
        public int AppointmentId { get; set; }
        
        [DisplayName("Radiology Test")]
        public int RadiologyTypeId { get; set; }
        
        [DisplayName("Is Internal")]
        public bool IsInternal { get; set; }

        public virtual Appointment? AppointmentIdNavigation { get; set; }
        public virtual RadiologyType? RadiologyTypeIdNavigation { get; set; }
    }
}
