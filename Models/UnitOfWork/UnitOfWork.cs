using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;

namespace Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private readonly ClinicDbContext context;

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
    public IRadiologyOrderRepository RadiologyOrderRepository { get; }
    public IMedicineOrderRepository MedicineOrderRepository { get; }


    public UnitOfWork(ClinicDbContext context,

        IEmployeeRepository employeeRepository,
        IEmployeeTypeRepository employeeTypeRepository,
        IDoctorSpecializionRepository doctorSpecializionRepository,
        IEmpDoctorSpecializionRepository empDoctorSpecializionRepository,
        IDoctorScheduleRepository doctorScheduleRepository,

        IPatientRepository patientRepository,
        IPatientTypeRepository patientTypeRepository,
        IAppointmentTypeRepository appointmentTypeRepository,
        IAppointmentRepository appointmentRepository,
        IPaymentMethodRepository paymentMethodRepository,
        IFeeRepository feeRepository,

        IMedicineRepository medicineRepository,
        IDosageRepository dosageRepository,

        ILabratoryTypeRepository labratoryTypeRepository,
        IRadiologyTypeRepository radiologyTypeRepository,
        ILaboratoryRecordRepository laboratoryRecordRepository,
        IRadiologyRecordRepository radiologyRecordRepository,

        IPatientVitalRepository patientVitalRepository,
        ILaboratoryOrderRepository laboratoryOrderRepository,
        IRadiologyOrderRepository radiologyOrderRepository,
        IMedicineOrderRepository medicineOrderRepository
        )
    {
        this.context = context;

        EmployeeRepository = employeeRepository;
        EmployeeTypeRepository = employeeTypeRepository;
        DoctorSpecializionRepository = doctorSpecializionRepository;
        EmpDoctorSpecializionRepository = empDoctorSpecializionRepository;
        DoctorScheduleRepository = doctorScheduleRepository;

        PatientRepository = patientRepository;
        PatientTypeRepository = patientTypeRepository;
        AppointmentTypeRepository = appointmentTypeRepository;
        AppointmentRepository = appointmentRepository;
        PaymentMethodRepository = paymentMethodRepository;
        FeeRepository = feeRepository;

        MedicineRepository = medicineRepository;
        DosageRepository = dosageRepository;

        LabratoryTypeRepository = labratoryTypeRepository;
        RadiologyTypeRepository = radiologyTypeRepository;
        LaboratoryRecordRepository = laboratoryRecordRepository;
        RadiologyRecordRepository = radiologyRecordRepository;

        PatientVitalRepository = patientVitalRepository;
        LaboratoryOrderRepository = laboratoryOrderRepository;
        RadiologyOrderRepository = radiologyOrderRepository;
        MedicineOrderRepository = medicineOrderRepository;

    }

    public void Savechanges()   
    {
        try
        {
            context.SaveChanges();

        }catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
