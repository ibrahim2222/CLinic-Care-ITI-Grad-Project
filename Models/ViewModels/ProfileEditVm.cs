using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clinc_Care_MVC_Grad_PROJ.Models.ViewModels
{
    public class ProfileEditVm
    {
        [DisplayName("Birth Date")]
        public DateTime PatientBirthDate { get; set; }

        [DisplayName("Gender")]
        public Gender PatientGender { get; set; }

        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "start with 010 | 011 | 012 | 015 and max 11 Diget")]
        [MaxLength(11)]
        [DisplayName("Phone Number")]
        public string PatientPhone { get; set; }

    }
}
