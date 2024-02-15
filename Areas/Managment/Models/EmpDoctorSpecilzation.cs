using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class EmpDoctorSpecilzation
    {
        public int EmpDoctorSpecilzationId { get; set; }

        [DisplayName("Doctor")]
        public int DoctorId { get; set; }
        
        [DisplayName("Specilzation")]
        public int DoctorSpecilzationId { get; set; }

        public virtual Employee? DoctorIdNavigation { get; set; }
        public virtual DoctorSpecializion? DoctorSpecilzationIdNavigation { get; set; }
    }
}