using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Microsoft.AspNetCore.Identity;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class EmployeeRepoService : IEmployeeRepository
    {
        public ClinicDbContext Ctx { get; }
        public UserManager<AppUser> UserManager { get; }

        public EmployeeRepoService(ClinicDbContext ctx, UserManager<AppUser> userManager)
        {
            Ctx = ctx;
            UserManager = userManager;
        }
        public List<Employee> GetAllEmployees()
        {
            return Ctx.Employees.ToList();
        }
        public Employee GetEmployeeById(int id)
        {
            var data = Ctx.Employees.Where(emp => emp.EmpId == id).SingleOrDefault();
            try
            {
                if (data == null)
                {
                    throw new ArgumentException($"Can't Find That Employee with Id: {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return data;
        }

        public void InsertEmployee(Employee employee)
        {
            if (employee != null)
            {
                try
                {
                    Ctx.Employees.Add(employee);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void UpdateEmployee(int id, Employee employee)
        {
            var updatedemployee = GetEmployeeById(id);
            if (employee != null)
            {
                try
                {
                    updatedemployee.EmpFirstName = employee.EmpFirstName;
                    updatedemployee.EmpLastName = employee.EmpLastName;
                    updatedemployee.EmpBirthDate = employee.EmpBirthDate;
                    updatedemployee.EmpGender = employee.EmpGender;
                    updatedemployee.EmpPhone = employee.EmpPhone;
                    updatedemployee.EmpAddress = employee.EmpAddress;
                    updatedemployee.EmpEmail = employee.EmpEmail;
                    updatedemployee.EmpPassword = employee.EmpPassword;
                    updatedemployee.EmpNationalId = employee.EmpNationalId;
                    updatedemployee.EmpHirigDate = employee.EmpHirigDate;
                    updatedemployee.EmpSalary= employee.EmpSalary;
                    updatedemployee.EmployeeTypeId = employee.EmployeeTypeId;
                    updatedemployee.UpdatedDate = DateTime.Now;

                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public async Task DeleteEmployee(int id)
        {
            var deletedEmployee = GetEmployeeById(id);
            AppUser userFromDB = await UserManager.FindByEmailAsync(deletedEmployee.EmpEmail);
            if (deletedEmployee != null)
            {
                try
                {
                    deletedEmployee.IsDeleted = true;
                    deletedEmployee.UpdatedDate = DateTime.Now;
                    userFromDB.EmailConfirmed = false;

                    if (deletedEmployee.EmployeeTypeIdNavigation.TypeName.ToLower() == "doctor")
                    {
                        var DeletedOldEmpDocSpec = Ctx.EmpDoctorSpecilzations.Where(c => c.DoctorId == id).Select(c => new
                        {
                            c.EmpDoctorSpecilzationId,
                            c.DoctorId,
                            c.DoctorSpecilzationId
                        }).SingleOrDefault();
                        DeletedEmpDoctorSpecilzation deletedEmpDoctorSpecilzation = new DeletedEmpDoctorSpecilzation()
                        {
                            DoctorId = DeletedOldEmpDocSpec.DoctorId,
                            DoctorSpecilzationId = DeletedOldEmpDocSpec.DoctorSpecilzationId
                        };
                        Ctx.DeletedEmpDoctorSpecilzations.Add(deletedEmpDoctorSpecilzation);

                        var delete = Ctx.EmpDoctorSpecilzations.Find(DeletedOldEmpDocSpec.EmpDoctorSpecilzationId);
                        Ctx.EmpDoctorSpecilzations.Remove(delete);
                    }
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
