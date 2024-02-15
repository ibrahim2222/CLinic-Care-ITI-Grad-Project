using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;

namespace Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment
{
    public interface IEmployeeRepository
    {
        public List<Employee> GetAllEmployees();
        public Employee GetEmployeeById(int id);
        public void InsertEmployee(Employee employee);
        public void UpdateEmployee(int id, Employee employee);
        public Task DeleteEmployee(int id);
    }
}
