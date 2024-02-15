using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.ViewModels
{
    public class AppPatientOrdersVm
    {
        public int AppointmentId { get; set; }
        public List<int>? ListofOrders { get; set; } = new List<int>();
    }
}
