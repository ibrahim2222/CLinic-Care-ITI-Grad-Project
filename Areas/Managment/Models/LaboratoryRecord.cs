using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class LaboratoryRecord
    {
        public int LaboratoryRecordId { get; set; }
        
        [DisplayName("Lab Result")]
        public string LabResult { get; set; }
        
        [DisplayName("Lab Result Date")]
        public DateTime LabRecordsDate { get; set; }

        [DisplayName("Appointment")]
        public int AppointmentId { get; set; } 

        public virtual Appointment? AppointmentIdNavigation { get; set; }
    }
}