using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Models.ViewModels
{
    public class AppoRegVm
    {
        [DisplayName("Specialization")]
        public int SpecializationID { get; set; }

        [DisplayName("Appoitment Date")]
        public DateTime AppoitmentDate { get; set; }

        [DisplayName("Is Cash")]
        public int IsCash { get; set; }
        
        [DisplayName("Fees")]
        public int Fees { get; set; }
    }
}
