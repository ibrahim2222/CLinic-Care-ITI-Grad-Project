using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class LabTypeRepoService : ILabratoryTypeRepository
    {
        public ClinicDbContext Context { get; set; }
        public LabTypeRepoService(ClinicDbContext dbContext) 
        {
            Context = dbContext;
        }

        public List<LaboratoryType> GetAllLabTypes()
        {
            return Context.LaboratoryTypes.ToList();
        }

        public LaboratoryType GetLabTypeById(int id)
        {
            var data = Context.LaboratoryTypes.Where(L => L.LabTypeId == id).SingleOrDefault();
            try
            {
                if (data == null)
                    throw new ArgumentException($"This record does not exist with id :{id}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return data;
        }

        public void InsertLabType(LaboratoryType laboratoryType)
        {
            if(laboratoryType != null)
            {
                try
                {
                    Context.LaboratoryTypes.Add(laboratoryType);
                    Context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void UpdateLabType(int id, LaboratoryType laboratoryType)
        {
            var labType = GetLabTypeById(id);
            if(laboratoryType != null)
            {
                try
                {
                    labType.LabTypeId = id;
                    labType.LabName = laboratoryType.LabName;

                    Context.SaveChanges();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
               
                
            }
        }

        public void DeleteLabType(int id)
        {
            var deletedLab = GetLabTypeById(id);
            if(deletedLab != null)
            {
                try
                {
                    Context.LaboratoryTypes.Remove(deletedLab);
                    Context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

        }
    }
}
