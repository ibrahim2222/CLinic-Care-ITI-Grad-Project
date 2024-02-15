using System.ComponentModel.DataAnnotations;

namespace Clinc_Care_MVC_Grad_PROJ.Models.ViewModels
{
    public class ResetPasswordModel
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")] 
        public string Email { get; set; }
        public string Token { get; set; }

    }
}
