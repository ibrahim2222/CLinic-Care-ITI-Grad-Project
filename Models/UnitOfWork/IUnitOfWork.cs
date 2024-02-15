using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;

namespace Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
public interface IUnitOfWork
{
    public IEmployeeRepository EmployeeRepository { get; }
    public IEmployeeTypeRepository EmployeeTypeRepository { get; }
    public IDoctorSpecializionRepository DoctorSpecializionRepository { get; }
    public IEmpDoctorSpecializionRepository EmpDoctorSpecializionRepository { get; }
    public IDoctorScheduleRepository DoctorScheduleRepository { get; }

    public IPatientRepository PatientRepository { get; }
    public IPatientTypeRepository PatientTypeRepository { get; }
    public IAppointmentTypeRepository AppointmentTypeRepository { get; }
    public IAppointmentRepository AppointmentRepository { get; }
    public IPaymentMethodRepository PaymentMethodRepository { get; }
    public IFeeRepository FeeRepository { get; }

    public IMedicineRepository MedicineRepository { get; }
    public IDosageRepository DosageRepository { get; }

    public ILabratoryTypeRepository LabratoryTypeRepository { get; }
    public IRadiologyTypeRepository RadiologyTypeRepository { get; }
    public ILaboratoryRecordRepository LaboratoryRecordRepository { get; }
    public IRadiologyRecordRepository RadiologyRecordRepository { get; }

    public IPatientVitalRepository PatientVitalRepository { get; }
    public ILaboratoryOrderRepository LaboratoryOrderRepository { get; }
    public IMedicineOrderRepository MedicineOrderRepository { get; }
    public IRadiologyOrderRepository RadiologyOrderRepository { get; }


    void Savechanges();
}
