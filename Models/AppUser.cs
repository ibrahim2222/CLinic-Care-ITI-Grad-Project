using Microsoft.AspNetCore.Identity;

namespace Clinc_Care_MVC_Grad_PROJ.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
