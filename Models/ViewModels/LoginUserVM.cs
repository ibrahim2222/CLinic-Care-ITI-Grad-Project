using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clinc_Care_MVC_Grad_PROJ.Models.ViewModels
{
    public class LoginUserVM
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        [DisplayName("Login Email")]
        public string LoginEmail { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Login Password")]
        public string LoginPassword { get; set; }

        [DisplayName("RemeberMe")]
        public bool RemeberMe { get; set; }
    }
}
