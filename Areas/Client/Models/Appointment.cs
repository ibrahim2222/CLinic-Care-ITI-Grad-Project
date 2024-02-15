using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models
{
    public class Appointment
    {
        public Appointment()
        {
            PatientVitals = new HashSet<PatientVital>();
            LaboratoryOrders = new HashSet<LaboratoryOrder>();
            RadiologyOrders = new HashSet<RadiologyOrder>();
            MedicineOrders = new HashSet<MedicineOrder>();
            LaboratoryRecords = new HashSet<LaboratoryRecord>();
            RadiologyRecords = new HashSet<RadiologyRecord>();
        }

        public int AppointmentId { get; set; }

        [DisplayName("Appointment Type")]
        public int AppointmentTypeId { get; set; }

        [DisplayName("Payment Mathod")]
        public int PaymentMathodId { get; set; }

        [DisplayName("Patient")]
        public int PatientId { get; set; }

        [DisplayName("Doctor")]
        public int DoctorId { get; set; }

        [DisplayName("Examination Comment")]
        public string? ExaminationComment { get; set; }

        [DisplayName("Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [DisplayName("Created Appointment Date")]
        public DateTime CreatedAppointmentDate { get; set; }
        
        [DisplayName("Updated Appointment Date")]
        public DateTime UpdatedAppointmentDate { get; set; }

        [DisplayName("Is Canceled")]
        public bool IsCanceled { get; set; }

        [DisplayName("Is Paid")]
        public bool IsPaid { get; set; }
        
        [DisplayName("Appointment State")]
        public AppointmentStates AppointmentState { get; set; }

        public virtual AppointmentType? AppointmentTypeIdNavigation { get; set; }
        public virtual PaymentMethod? PaymentMethodIdNavigation { get; set; }
        public virtual Patient? PatientIdNavigation { get; set; }
        public virtual Employee? DoctorIdNavigation { get; set; }
    
        public virtual ICollection<PatientVital> PatientVitals { get; set; }
        public virtual ICollection<LaboratoryOrder>? LaboratoryOrders { get; set; }
        public virtual ICollection<LaboratoryRecord>? LaboratoryRecords { get; set; }
        public virtual ICollection<RadiologyOrder>? RadiologyOrders { get; set; }
        public virtual ICollection<RadiologyRecord>? RadiologyRecords { get; set; }
        public virtual ICollection<MedicineOrder>? MedicineOrders { get; set; }
    }

    public enum AppointmentStates
    {
        reserverd = 1,
        queued = 2,
        ready = 3,
        treated = 4,
        canceled = 5
    }
}
