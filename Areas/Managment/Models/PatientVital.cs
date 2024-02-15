using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class PatientVital
    {
        public int VitalId { get; set; }

        [DisplayName("Blood Pressure")]
        [MinLength(6)]
        [MaxLength(6)]
        public string BloodPressure { get; set; }

        [DisplayName("Blood Type")]
        public BloodType BloodType { get; set; }

        [DisplayName("Heart Rate")]
        [Range(50, 110, ErrorMessage = "Heart Rate must be between {1} and {2}")]
        public int HeartRate { get; set; }

        [DisplayName("Temperature")]
        [Range(34, 43, ErrorMessage = "Temperature must be between {1} and {2}")]
        public double Temperature { get; set; }

        [DisplayName("Height")]
        [Range(40, 250, ErrorMessage = "Height must be between {1} and {2} in CM")]
        public double Height { get; set; }

        [DisplayName("Weight")]
        [Range(2.5, 500, ErrorMessage = "Weight must be between {1} and {2} in KG")]
        public double Weight { get; set; }

        [DisplayName("Patient Comment")]
        public string PatientComment { get; set; }

        [DisplayName("Appointment")]
        public int AppointmentId { get; set; }

        [DisplayName("Nurse")]
        public int NurseId { get; set; }

        public virtual Appointment? AppointmentIdNavigation { get; set; }
        public virtual Employee? NurseIdNavigation { get; set; }
    }
    public enum BloodType
    {
        [EnumMember(Value = "A+")]
        APositive,

        [EnumMember(Value = "A-")]
        ANegative,

        [EnumMember(Value = "B+")]
        BPositive,

        [EnumMember(Value = "B-")]
        BNegative,

        [EnumMember(Value = "AB+")]
        ABPositive,

        [EnumMember(Value = "AB-")]
        ABNegative,

        [EnumMember(Value = "O+")]
        OPositive,

        [EnumMember(Value = "O-")]
        ONegative
    }
}
