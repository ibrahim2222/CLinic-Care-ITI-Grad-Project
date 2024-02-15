using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class LaboratoryType
    {
        public LaboratoryType()
        {
            LaboratoryOrders = new HashSet<LaboratoryOrder>();
        }

        public int LabTypeId { get; set; }

        [DisplayName("Lab Test Name")]
        public string LabName { get; set; }

        public virtual ICollection<LaboratoryOrder> LaboratoryOrders { get; set; }
    }
}
