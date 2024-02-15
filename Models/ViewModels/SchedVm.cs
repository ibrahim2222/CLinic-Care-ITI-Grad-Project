using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Models.ViewModels
{
    public class SchedVm
    {
        [DisplayName("Doctor")]
        public int DoctorId { get; set; }

        [DisplayName("Days Of Week")]
        public List<int>? DaysOfWeek { get; set; } 

        public virtual Employee? DoctorIdNavigation { get; set; }

    }
}
