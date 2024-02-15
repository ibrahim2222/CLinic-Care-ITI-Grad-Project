using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Validations;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class Employee
    {
        public Employee()
        {
            EmpDoctorSpecilzations = new HashSet<EmpDoctorSpecilzation>();
            DeletedEmpDoctorSpecilzations = new HashSet<DeletedEmpDoctorSpecilzation>();
            DoctorSchedules = new HashSet<DoctorSchedule>();
            PatientVitals = new HashSet<PatientVital>();
            Appointments = new HashSet<Appointment>();
        }

        public int EmpId { get; set; }

        [NotMapped]
        [DisplayName("Full Name")]
        public string EmployeeFullName
        {
            get { return $"{EmpFirstName} {EmpLastName}"; }
        }

        [DisplayName("First Name")]
        public string EmpFirstName { get; set; }
        
        [DisplayName("Last Name")]
        public string EmpLastName { get; set; }
        
        [DisplayName("Birth Date")]
        public DateTime EmpBirthDate { get; set; }
        
        [DisplayName("Gender")]
        public Gender  EmpGender { get; set; }

        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "start with 010 | 011 | 012 | 015 and max 11 Diget")]
        [MaxLength(11)]
        [DisplayName("Phone Number")]
        public string EmpPhone { get; set; }
        
        [DisplayName("Address")]
        public string EmpAddress { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        [DisplayName("Email")]
        public string? EmpEmail { get; set;}

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string? EmpPassword { get; set;}

        [RegularExpression(@"^([1-9])\d{13}$", ErrorMessage = "max 14 Diget")]
        [MaxLength(14)]
        [DisplayName("National ID")]
        public string EmpNationalId { get; set; }
        
        [DisplayName("Hirig Date")]
        [DateRange("01/01/2010")]
        public DateTime EmpHirigDate { get; set; }
        
        [DisplayName("Updated Date")]
        public DateTime UpdatedDate { get; set; }
        
        [DisplayName("Salary")]
        public double EmpSalary { get; set; }

        [DisplayName("Employee Type")]
        public int EmployeeTypeId { get; set; }

        [DisplayName("Is Deleted")]
        public bool IsDeleted { get; set; }

        [NotMapped]
        [DisplayName("Role")]
        public string RoleName { get; set; }

        public virtual EmployeeType? EmployeeTypeIdNavigation { get; set; }
    
        public virtual ICollection<EmpDoctorSpecilzation> EmpDoctorSpecilzations { get; set; }
        public virtual ICollection<DeletedEmpDoctorSpecilzation> DeletedEmpDoctorSpecilzations { get; set; }
        public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; }
        
        public virtual ICollection<PatientVital> PatientVitals { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}


