using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models
{
    public class AppointmentType
    {
        public AppointmentType()
        {
            Appointments = new HashSet<Appointment>();
        }
        public int AppointmentTypeId { get; set; }
        
        [DisplayName("Type Name")]
        public string TypeName { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
