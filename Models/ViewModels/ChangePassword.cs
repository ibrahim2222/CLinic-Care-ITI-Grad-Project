using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clinc_Care_MVC_Grad_PROJ.Models.ViewModels
{
    public class ChangePassword
    {
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
 
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "start with 010 | 011 | 012 | 015 and max 11 Diget")]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }
    }
}
