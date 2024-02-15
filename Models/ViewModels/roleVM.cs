using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Clinc_Care_MVC_Grad_PROJ.Models.ViewModels
{
    public class roleVM
    {
        [DisplayName("Role Name")]
        public string RoleName { get; set; }
    }
}
