using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class DoctorSpecializion
    {
        public DoctorSpecializion()
        {
            EmpDoctorSpecilzations = new HashSet<EmpDoctorSpecilzation>();
            DeletedEmpDoctorSpecilzations = new HashSet<DeletedEmpDoctorSpecilzation>();
        }

        public int SpecializationId { get; set; }

        [DisplayName("Specialization Name")]
        public string SpecializationName { get; set; }

        public virtual ICollection<EmpDoctorSpecilzation> EmpDoctorSpecilzations { get; set; }
        public virtual ICollection<DeletedEmpDoctorSpecilzation> DeletedEmpDoctorSpecilzations { get; set; }


    }
}

