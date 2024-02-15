using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Models.ViewModels
{
    public class EmpDoctorVM : Employee
    {
        [DisplayName("Doctor Specializion")]
        public int DoctorSpecializion { get; set; }
    }
}
