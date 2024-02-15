using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class RadiologyType
    {
        public RadiologyType()
        {
            RadiologyOrders = new HashSet<RadiologyOrder>();
        }

        public int RadiologyTypeId { get; set; }
        
        [DisplayName("Radio Test Name")]
        public string RadiologyName { get; set; }

        public virtual ICollection<RadiologyOrder> RadiologyOrders { get; set; }
    }
}
