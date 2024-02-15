using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class RadiologyRecordRepoService : IRadiologyRecordRepository
    {
        public ClinicDbContext Context { get; set; }
        public RadiologyRecordRepoService(ClinicDbContext dbContext) 
        {
            Context = dbContext;
        }
      
        public List<RadiologyRecord> GetAllRadiologyRecord()
        {
            return Context.RadiologyRecords.ToList();
        }

        public RadiologyRecord GetRadiologyRecordById(int id)
        {
            var data = Context.RadiologyRecords.Where(R => R.RadiologyRecordId == id).SingleOrDefault();
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

        public void InsertRadiologyRecord(RadiologyRecord record)
        {
            if(record != null)
            {
                try
                {
                    Context.RadiologyRecords.Add(record);
                    Context.SaveChanges();
                }
                catch (Exception e) 
                {
                    Console.WriteLine(e.ToString());
                }

            }
        }

        public void UpdateRadiologyRecord(int Id, RadiologyRecord record)
        {
            var RadRecord = GetRadiologyRecordById(Id);
            if( record != null ) 
            {
                try
                {
                    RadRecord.RadioResult = record.RadioResult;
                    RadRecord.RadiologyRecordDate = record.RadiologyRecordDate;
                    RadRecord.AppointmentId = record.AppointmentId;

                    Context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void DeleteRadiologyRecord(int Id)
        {
            var deletedRecord = GetRadiologyRecordById(Id);
            if ( deletedRecord != null )
            {
                try
                {
                    Context.RadiologyRecords.Remove(deletedRecord);
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
