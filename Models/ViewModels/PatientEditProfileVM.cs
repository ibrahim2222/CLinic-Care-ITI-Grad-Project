using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clinc_Care_MVC_Grad_PROJ.Models.ViewModels
{
    public class PatientEditProfileVM
    {

        public int PatientId { get; set; }

        [DisplayName("Phone Number")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "start with 010 | 011 | 012 | 015 and max 11 Diget")]
        [MaxLength(11)]
        public string PatientPhone { get; set; }

        [DisplayName("Gender")]
        public Gender PatientGender { get; set; }


        [DisplayName("Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]

        public DateTime PatientBirthDate { get; set; }
    }
}
