using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class LaboratoryOrder
    {
        public int LaboratoryOrderId { get; set; }

        [DisplayName("Appointment")]
        public int AppointmentId { get; set; } 
        
        [DisplayName("Lab Test")]
        public int LabTypeId { get; set; }

        [DisplayName("Is Internal")]
        public bool IsInternal { get; set; }

        public virtual Appointment? AppointmentIdNavigation { get; set; }
        public virtual LaboratoryType? LaboratoryTypeIdNavigation { get; set; }
    }
}
