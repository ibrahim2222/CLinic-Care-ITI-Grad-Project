using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface IEmployeeTypeRepository
    {
        public List<EmployeeType> GetAllEmployeeType();
        public EmployeeType GetEmployeeTypeById(int id);
        public void InsertEmployeeType(EmployeeType employeetype);
        public void UpdateEmployeeType(int id, EmployeeType employeetype);
        public void DeleteEmployeeType(int id);
    }
}
