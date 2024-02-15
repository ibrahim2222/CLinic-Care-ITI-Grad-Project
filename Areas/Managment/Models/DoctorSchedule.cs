using System.ComponentModel;

namespace Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models
{
    public class DoctorSchedule
    {
        public int ScheduleId { get; set; }

        [DisplayName("Doctor")]
        public int DoctorId { get; set; }

        [DisplayName("Days Of Week")]
        public DaysOfWeek DaysOfWeek { get; set; }

        public virtual Employee? DoctorIdNavigation { get; set; }
    }

    [Flags]
    public enum DaysOfWeek
    {
        None = 0,         // No days selected
        Saturday = 1,      // 00000001
        Sunday = 2,        // 00000010
        Monday = 4,        // 00000100       
        Tuesday = 8,       // 00001000    
        Wednesday = 16,    // 00010000  
        Thursday = 32,     // 00100000    
        Friday = 64,       // 01000000


    }
}


