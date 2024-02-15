using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Validations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models
{
    public class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int PatientId { get; set; }
        [NotMapped]
        [DisplayName("Full Name")]
        public string PatientFullName
        {
            get { return $"{PatientFirstName} {PatientLastName}"; }
        }

        [DisplayName("First Name")]
        public string PatientFirstName { get; set; }

        [DisplayName("Last Name")]
        public string PatientLastName { get; set; }

        [DisplayName("Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [CustomMaxDate]
        public DateTime? PatientBirthDate { get; set; }

        [DisplayName("Gender")]
        public Gender? PatientGender { get; set; }

        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "start with 010 | 011 | 012 | 015 and max 11 Diget")]
        [MaxLength(11)]
        [DisplayName("Phone Number")]
        public string? PatientPhone { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        [DisplayName("Email")]
        public string? PatientEmail { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string? PatientPassword { get; set; }

        [DisplayName("Joined Date")]
        public DateTime JoinedDate { get; set; }

        [DisplayName("Patient Type")]
        public int PatientTypeId { get; set; }

        public virtual PatientType? PatientTypeIdNavigation { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
