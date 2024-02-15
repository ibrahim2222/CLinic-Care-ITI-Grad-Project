using System.ComponentModel.DataAnnotations;

namespace Clinc_Care_MVC_Grad_PROJ.Validations
{
    public class CustomMinDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateValue)
            {
                return dateValue >= DateTime.Today;
            }
            return false;
        }
    }
}
