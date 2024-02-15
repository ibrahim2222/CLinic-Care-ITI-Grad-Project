using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models
{
    public class Fee
    {
        public int FeeId { get; set; }

        [DisplayName("Fee Name")]
        public string FeeName { get; set; }

        [DisplayName("Fee Amount")]
        public double FeeAmount { get; set; }
    }
}
