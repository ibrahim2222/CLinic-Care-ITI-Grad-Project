using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Clinc_Care_MVC_Grad_PROJ.Models.ViewModels
{
    public class ExternalLoginModel
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public ClaimsPrincipal Principal { get; set; }
    }
}




