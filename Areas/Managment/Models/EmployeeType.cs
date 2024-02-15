using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class EmployeeType
    {
        public EmployeeType()
        {
            Employees = new HashSet<Employee>();
        }
        public int EmployeeTypeId { get; set; }
        
        [DisplayName("Employee Type Name")]
        public string TypeName { get; set; }

        public virtual ICollection<Employee> Employees { get; set;}
    }
}