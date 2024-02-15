using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class EmployeeTypeRepoService : IEmployeeTypeRepository
    {
        public ClinicDbContext Ctx { get; }
        public EmployeeTypeRepoService(ClinicDbContext ctx)
        {
            Ctx = ctx;
        }

        public List<EmployeeType> GetAllEmployeeType()
        {
            return Ctx.EmployeeTypes.ToList();
        }

        public EmployeeType GetEmployeeTypeById(int id)
        {
            var data = Ctx.EmployeeTypes.Where(emp => emp.EmployeeTypeId == id).SingleOrDefault();
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

        public void InsertEmployeeType(EmployeeType employeetype)
        {
            if (employeetype != null)
            {
                try
                {
                    Ctx.EmployeeTypes.Add(employeetype);
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void UpdateEmployeeType(int id, EmployeeType employeeType)
        {
            var updatedemployeeType = GetEmployeeTypeById(id);

            if (employeeType != null)
            {
                try
                {
                    updatedemployeeType.TypeName = employeeType.TypeName;
                    Ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void DeleteEmployeeType(int id)
        {
            var deletedEmployeeType = GetEmployeeTypeById(id);
            if (deletedEmployeeType != null)
            {
                try
                {
                    Ctx.EmployeeTypes.Remove(deletedEmployeeType);
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
