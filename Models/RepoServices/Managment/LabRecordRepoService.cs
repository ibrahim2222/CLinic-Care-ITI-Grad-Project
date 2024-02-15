using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;

namespace Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment
{
    public class LabRecordRepoService : ILaboratoryRecordRepository
    {
        public ClinicDbContext Context { get; set; }
        public LabRecordRepoService(ClinicDbContext dbContext) 
        {
            Context = dbContext;
        }
       
        public List<LaboratoryRecord> GetAllLabRecords()
        {
            return Context.LaboratoryRecords.ToList();
            
        }

        public LaboratoryRecord GetLabRecordById(int id)
        {
            var data = Context.LaboratoryRecords.Where(L => L.LaboratoryRecordId == id).SingleOrDefault();
            try
            {
                if(data == null)
                {
                    throw new ArgumentException($"This record does not exist with id :{id}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return data;
        }

        public void InsertLabRecord(LaboratoryRecord labRecord)
        {
            if(labRecord != null)
            {
                try
                {
                    Context.LaboratoryRecords.Add(labRecord);
                    Context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void UpdateLabRecord(int id, LaboratoryRecord labRecord)
        {
            var labR = GetLabRecordById(id);
            if(labRecord != null)
            {
                try
                {
                    labR.LabResult = labRecord.LabResult;
                    labR.LabRecordsDate = labRecord.LabRecordsDate;
                    labR.AppointmentId = labRecord.AppointmentId;

                    Context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
        public void DeleteLabRecord(int id)
        {
            var deletedLab = GetLabRecordById(id);

            if(deletedLab != null)
            {
                try
                {
                    Context.LaboratoryRecords.Remove(deletedLab);
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
