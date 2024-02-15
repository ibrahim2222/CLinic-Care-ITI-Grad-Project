using System.ComponentModel.DataAnnotations;

namespace Clinc_Care_MVC_Grad_PROJ.Models.ViewModels
{
    public class ForgotPasswordModel
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
