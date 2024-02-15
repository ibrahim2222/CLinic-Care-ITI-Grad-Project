using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models
{
    public class PatientType
    {
        public PatientType()
        {
            Patients = new HashSet<Patient>();
        }

        public int PatientTypeId { get; set; }

        [DisplayName("Patient Type Name")]
        public string PatientTypeName { get; set;}

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
