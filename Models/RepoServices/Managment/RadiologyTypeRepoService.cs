using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class RadiologyTypeRepoService : IRadiologyTypeRepository
    {
        public ClinicDbContext Context { get; set; }
        public RadiologyTypeRepoService(ClinicDbContext dbContext)
        {
            Context = dbContext;
        }
      
        public List<RadiologyType> GetAllRadiologyType()
        {
            return Context.RadiologyTypes.ToList();
        }

        public RadiologyType GetRadiologyTypeById(int id)
        {
            var data = Context.RadiologyTypes.Where(R => R.RadiologyTypeId == id).SingleOrDefault();
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

        public void InsertRadiologyType(RadiologyType radiologyType)
        {
            if(radiologyType != null)
            {
                try
                {
                    Context.RadiologyTypes.Add(radiologyType);
                    Context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void UpdateRadiologyType(int id, RadiologyType radiologyType)
        {
            var radType = GetRadiologyTypeById(id); 
            if (radiologyType != null)
            {
                try
                {
                    radType.RadiologyName = radiologyType.RadiologyName;
                    Context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void DeleteRadiologyType(int id)
        {
            var deletedRadiology = GetRadiologyTypeById(id);
            if(deletedRadiology != null)
            {
                try
                {
                    Context.RadiologyTypes.Remove(deletedRadiology);
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
