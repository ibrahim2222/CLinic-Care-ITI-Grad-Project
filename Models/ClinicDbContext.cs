using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;
using Clinc_Care_MVC_Grad_PROJ.Areas.Managment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Clinc_Care_MVC_Grad_PROJ.Models
{
    public class ClinicDbContext : IdentityDbContext<AppUser>
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }


        // Employee
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<DoctorSpecializion> DoctorSpecializions { get; set; }
        public DbSet<EmpDoctorSpecilzation> EmpDoctorSpecilzations { get; set; }
        public DbSet<DeletedEmpDoctorSpecilzation> DeletedEmpDoctorSpecilzations { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }

        // Patient
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientType> PatientTypes { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Fee> Fees { get; set; }

        // Medicine
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Dosage> Dosages { get; set; }

        // Exminations Records
        public DbSet<LaboratoryType> LaboratoryTypes { get; set; }
        public DbSet<RadiologyType> RadiologyTypes { get; set; }
        public DbSet<LaboratoryRecord> LaboratoryRecords { get; set; }
        public DbSet<RadiologyRecord> RadiologyRecords { get; set; }

        // Exminations Orders
        public DbSet<PatientVital> PatientVitals { get; set; }
        public DbSet<LaboratoryOrder> LaboratoryOrders { get; set; }
        public DbSet<RadiologyOrder> RadiologyOrders { get; set; }
        public DbSet<MedicineOrder> MedicineOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Employee
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId);

                entity.ToTable("employee", "employee");

                entity.Property(e => e.EmpFirstName)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);

                entity.Property(e => e.EmpLastName)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);

                entity.Property(e => e.EmpBirthDate)
                    .IsRequired(true)
                    .HasColumnType("datetime2");

                entity.Property(e => e.EmpGender)
                    .IsRequired(true);

                entity.Property(e => e.EmpPhone)
                    .HasMaxLength(11)
                    .IsRequired(true)
                    .IsUnicode(false);

                entity.Property(e => e.EmpAddress)
                    .HasMaxLength(100)
                    .IsRequired(true)
                    .IsUnicode(false);

                entity.Property(e => e.EmpEmail)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);

                entity.Property(e => e.EmpPassword)
                    .HasColumnType("varchar(max)")
                    .IsRequired(false)
                    .IsUnicode(false);

                entity.Property(e => e.EmpNationalId)
                    .HasMaxLength(14)
                    .IsRequired(true)
                    .IsUnicode(false);

                entity.Property(e => e.EmpHirigDate)
                    .IsRequired(true)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");
                
                entity.Property(e => e.UpdatedDate)
                    .IsRequired(true)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.EmpSalary)
                    .IsRequired(true)
                    .HasColumnType("decimal");

                entity.Property(e => e.IsDeleted)
                   .IsRequired(true);
                //.HasDefaultValueSql("0");

                entity.HasIndex(e => e.EmpEmail).IsUnique(true);

                entity.HasIndex(e => e.EmpPhone).IsUnique(true);

                entity.HasIndex(e => e.EmpNationalId).IsUnique(true);

                entity.HasOne(e => e.EmployeeTypeIdNavigation)
                    .WithMany(c => c.Employees)
                    .HasForeignKey(e => e.EmployeeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_employee_empType");
            });

            modelBuilder.Entity<EmployeeType>(entity =>
            {
                entity.HasKey(e => e.EmployeeTypeId);

                entity.ToTable("employeeType", "employee");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DoctorSpecializion>(entity =>
            {
                entity.HasKey(e => e.SpecializationId);

                entity.ToTable("doctorSpecializion", "employee");

                entity.Property(e => e.SpecializationName)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmpDoctorSpecilzation>(entity =>
            {
                entity.HasKey(e => e.EmpDoctorSpecilzationId);

                entity.ToTable("empDoctorSpecilzation", "employee");

                entity.HasIndex(e => e.DoctorId).IsUnique(true);

                entity.HasIndex(e => e.DoctorSpecilzationId).IsUnique(true);

                entity.HasIndex(e => new
                {
                    e.DoctorId,
                    e.DoctorSpecilzationId
                }).IsUnique(true);

                entity.HasOne(e => e.DoctorIdNavigation)
                    .WithMany(c => c.EmpDoctorSpecilzations)
                    .HasForeignKey(e => e.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_empDoctorSpecilzation_employee");

                entity.HasOne(e => e.DoctorSpecilzationIdNavigation)
                    .WithMany(c => c.EmpDoctorSpecilzations)
                    .HasForeignKey(e => e.DoctorSpecilzationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_empDoctorSpecilzation_doctorSpecializion");
            });
            
            modelBuilder.Entity<DeletedEmpDoctorSpecilzation>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.ToTable("deletedempDoctorSpecilzation", "employee");

                entity.Property(e => e.CreatedDate)
                    .IsRequired(true)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.DoctorIdNavigation)
                    .WithMany(c => c.DeletedEmpDoctorSpecilzations)
                    .HasForeignKey(e => e.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_deletedempDoctorSpecilzation_employee");

                entity.HasOne(e => e.DoctorSpecilzationIdNavigation)
                    .WithMany(c => c.DeletedEmpDoctorSpecilzations)
                    .HasForeignKey(e => e.DoctorSpecilzationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_deletedempDoctorSpecilzation_doctorSpecializion");
            });

            modelBuilder.Entity<DoctorSchedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId);

                entity.ToTable("doctorSchedule", "employee");

                entity.Property(e => e.DaysOfWeek)
                    .IsRequired(true);

                entity.HasIndex(e => e.DoctorId).IsUnique(true);

                entity.HasOne(e => e.DoctorIdNavigation)
                    .WithMany(c => c.DoctorSchedules)
                    .HasForeignKey(e => e.DoctorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_doctorSchedule_employee");
            });

            // Patient
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.ToTable("patient", "patient");

                entity.Property(e => e.PatientFirstName)
                   .HasMaxLength(50)
                   .IsRequired(true)
                   .IsUnicode(false);

                entity.Property(e => e.PatientLastName)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);

                entity.Property(e => e.PatientBirthDate)
                    .IsRequired(false)
                    .HasColumnType("datetime2");

                entity.Property(e => e.PatientGender)
                    .IsRequired(false);

                entity.Property(e => e.PatientPhone)
                    .HasMaxLength(11)
                    .IsRequired(false)
                    .IsUnicode(false);

                entity.Property(e => e.PatientEmail)
                    .HasMaxLength(50)
                    .IsRequired(false)
                    .IsUnicode(false);

                entity.Property(e => e.PatientPassword)
                    .HasColumnType("varchar(max)")
                    .IsRequired(false)
                    .IsUnicode(false);

                entity.HasIndex(e => e.PatientEmail).IsUnique(true);

                entity.HasIndex(e => e.PatientPhone).IsUnique(true);

                entity.HasOne(e => e.PatientTypeIdNavigation)
                    .WithMany(c => c.Patients)
                    .HasForeignKey(e => e.PatientTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_patient_patientType");

                entity.Property(e => e.JoinedDate)
                    .IsRequired(true)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<PatientType>(entity =>
            {
                entity.HasKey(e => e.PatientTypeId);

                entity.ToTable("patientType", "patient");

                entity.Property(e => e.PatientTypeName)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => e.PaymentMethodId);

                entity.ToTable("paymentMethod", "patient");

                entity.Property(e => e.PaymentMethodName)
                   .HasMaxLength(50)
                   .IsRequired(true)
                   .IsUnicode(false);
            });

            modelBuilder.Entity<Fee>(entity =>
            {
                entity.HasKey(e => e.FeeId);

                entity.ToTable("fee", "patient");

                entity.Property(e => e.FeeName)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);

                entity.Property(e => e.FeeAmount)
                    .IsRequired(true)
                    .HasColumnType("decimal");
            });

            modelBuilder.Entity<AppointmentType>(entity =>
            {
                entity.HasKey(e => e.AppointmentTypeId);

                entity.ToTable("appointmentType", "patient");

                entity.Property(e => e.TypeName)
                   .HasMaxLength(50)
                   .IsRequired(true)
                   .IsUnicode(false);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.AppointmentId);

                entity.ToTable("appointment", "patient");

                entity.HasOne(e => e.AppointmentTypeIdNavigation)
                    .WithMany(c => c.Appointments)
                    .HasForeignKey(e => e.AppointmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_appoinment_appointmentType");

                entity.HasOne(e => e.PatientIdNavigation)
                    .WithMany(c => c.Appointments)
                    .HasForeignKey(e => e.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_appoinment_patient");

                entity.HasOne(e => e.DoctorIdNavigation)
                    .WithMany(c => c.Appointments)
                    .HasForeignKey(e => e.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_appoinment_doctorId");

                entity.HasOne(e => e.PaymentMethodIdNavigation)
                    .WithMany(c => c.Appointments)
                    .HasForeignKey(e => e.PaymentMathodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_appoinment_paymentMethod");

                entity.Property(e => e.ExaminationComment)
                    .HasColumnType("varchar(max)")
                    .IsRequired(false)
                    .IsUnicode(false);

                entity.Property(e => e.AppointmentDate)
                    .IsRequired(true)
                    .HasColumnType("datetime2");

                entity.Property(e => e.CreatedAppointmentDate)
                    .IsRequired(true)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdatedAppointmentDate)
                    .IsRequired(true)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.IsCanceled)
                    .IsRequired(true);
                //.HasDefaultValueSql("0");

                entity.Property(e => e.IsPaid)
                    .IsRequired(true);
                //.HasDefaultValueSql("0");

                entity.Property(e => e.AppointmentState)
                    .IsRequired(true)
                    .HasDefaultValue(AppointmentStates.reserverd);

            });

            // Medicine
            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.HasKey(e => e.MedicineId);

                entity.ToTable("medicine", "medicine");

                entity.Property(e => e.MedicineName)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);

                entity.Property(e => e.MedicineDescription)
                    .HasColumnType("varchar(max)")
                    .IsRequired(true)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dosage>(entity =>
            {
                entity.HasKey(e => e.DosageId);

                entity.ToTable("dosage", "medicine");

                entity.Property(e => e.DosageName)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);
            });

            // Exminations Records
            modelBuilder.Entity<LaboratoryType>(entity =>
            {
                entity.HasKey(e => e.LabTypeId);

                entity.ToTable("laboratoryType", "exRec");

                entity.Property(e => e.LabName)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RadiologyType>(entity =>
            {
                entity.HasKey(e => e.RadiologyTypeId);

                entity.ToTable("radiologyType", "exRec");

                entity.Property(e => e.RadiologyName)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RadiologyRecord>(entity =>
            {
                entity.HasKey(e => e.RadiologyRecordId);

                entity.ToTable("radioRec", "exRec");

                entity.Property(e => e.RadioResult)
                    .HasColumnType("varchar(max)")
                    .IsRequired(true)
                    .IsUnicode(false);

                entity.Property(e => e.RadiologyRecordDate)
                    .IsRequired(true)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.AppointmentIdNavigation)
                    .WithMany(c => c.RadiologyRecords)
                    .HasForeignKey(e => e.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_radiologyRecord_appointment");
            });

            modelBuilder.Entity<LaboratoryRecord>(entity =>
            {
                entity.HasKey(e => e.LaboratoryRecordId);

                entity.ToTable("labRec", "exRec");

                entity.Property(e => e.LabResult)
                    .HasColumnType("varchar(max)")
                    .IsRequired(true)
                    .IsUnicode(false);

                entity.Property(e => e.LabRecordsDate)
                    .IsRequired(true)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.AppointmentIdNavigation)
                    .WithMany(c => c.LaboratoryRecords)
                    .HasForeignKey(e => e.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_laboratoryRecord_appointment");
            });

            // Exminations Orders
            modelBuilder.Entity<PatientVital>(entity =>
            {
                entity.HasKey(e => e.VitalId);

                entity.ToTable("patientVital", "exOrder");

                entity.Property(e => e.BloodPressure)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(false);
                
                entity.Property(e => e.BloodType)
                    .HasConversion(new EnumToStringConverter<BloodType>())
                    .IsRequired(true)
                    .IsUnicode(false);

                entity.Property(e => e.HeartRate)
                    .IsRequired(true);

                entity.Property(e => e.Temperature)
                    .IsRequired(true)
                    .HasColumnType("decimal");

                entity.Property(e => e.Height)
                    .IsRequired(true)
                    .HasColumnType("decimal");

                entity.Property(e => e.Weight)
                    .IsRequired(true)
                    .HasColumnType("decimal");

                entity.Property(e => e.PatientComment)
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(false);

                entity.HasOne(e => e.AppointmentIdNavigation)
                    .WithMany(c => c.PatientVitals)
                    .HasForeignKey(e => e.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_patientVital_appointment");

                entity.HasOne(e => e.NurseIdNavigation)
                    .WithMany(c => c.PatientVitals)
                    .HasForeignKey(e => e.NurseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_patientVital_nurse");
            });

            modelBuilder.Entity<LaboratoryOrder>(entity =>
            {
                entity.HasKey(e => e.LaboratoryOrderId);

                entity.ToTable("laboratoryOrder", "exOrder");

                entity.Property(e => e.IsInternal)
                    .IsRequired(true)
                    .HasDefaultValueSql("1");

                entity.HasOne(e => e.AppointmentIdNavigation)
                    .WithMany(c => c.LaboratoryOrders)
                    .HasForeignKey(e => e.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_laboratoryOrder_appointment");

                entity.HasOne(e => e.LaboratoryTypeIdNavigation)
                    .WithMany(e => e.LaboratoryOrders)
                    .HasForeignKey(e => e.LabTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_laboratoryOrder_labType");
            });

            modelBuilder.Entity<RadiologyOrder>(entity =>
            {
                entity.HasKey(e => e.RadiologyOrderId);

                entity.ToTable("radiologyOrder", "exOrder");

                entity.Property(e => e.IsInternal)
                    .IsRequired(true)
                    .HasDefaultValueSql("1");

                entity.HasOne(e => e.AppointmentIdNavigation)
                    .WithMany(c => c.RadiologyOrders)
                    .HasForeignKey(e => e.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_radiologyOrder_appointment");

                entity.HasOne(e => e.RadiologyTypeIdNavigation)
                    .WithMany(e => e.RadiologyOrders)
                    .HasForeignKey(e => e.RadiologyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_radiologyOrder_radiologyType");
            });

            modelBuilder.Entity<MedicineOrder>(entity =>
            {
                entity.HasKey(e => e.MedicineOrderId);

                entity.ToTable("medicineOrder", "exOrder");

                entity.HasOne(e => e.MedicineIdNavigation)
                    .WithMany(c => c.MedicineOrders)
                    .HasForeignKey(e => e.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_medicineOrder_medicine");

                entity.HasOne(e => e.DosageIdNavigation)
                    .WithMany(c => c.MedicineOrders)
                    .HasForeignKey(e => e.DosageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_medicineOrder_dosage");

                entity.HasOne(e => e.AppointmentIdNavigation)
                    .WithMany(c => c.MedicineOrders)
                    .HasForeignKey(e => e.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_medicineOrder_appointment");

                entity.Property(e => e.Instructions)
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(false);
            });

            // Initialize Db

            // Employee
            modelBuilder.Entity<EmployeeType>().HasData(
                new EmployeeType
                {
                    EmployeeTypeId = 1,
                    TypeName = "Admin"
                },
                new EmployeeType
                {
                    EmployeeTypeId = 2,
                    TypeName = "Doctor"
                },
                new EmployeeType
                {
                    EmployeeTypeId = 3,
                    TypeName = "Nurse"
                },
                new EmployeeType
                {
                    EmployeeTypeId = 4,
                    TypeName = "Receptionist"
                },
                new EmployeeType
                {
                    EmployeeTypeId = 5,
                    TypeName = "Examination"
                });

            modelBuilder.Entity<DoctorSpecializion>().HasData(
                new DoctorSpecializion
                {
                    SpecializationId = 1,
                    SpecializationName = "Neurology"
                });

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmpId = 1,
                    EmpFirstName = "Admin",
                    EmpLastName = "Admin",
                    EmpBirthDate = new DateTime(2023, 09, 20),
                    EmpGender = Gender.Male,
                    EmpPhone = "01234567891",
                    EmpAddress = "Maadi",
                    EmpEmail = "admin@roshetaclinic.org",
                    EmpPassword = "Admin$123",
                    EmpNationalId = "12345678912345",
                    EmpHirigDate = new DateTime(2023, 09, 20),
                    EmpSalary = 10500.00,
                    EmployeeTypeId = 1
                },
                new Employee
                {
                    EmpId = 2,
                    EmpFirstName = "Admin",
                    EmpLastName = "Doctor",
                    EmpBirthDate = new DateTime(2023, 09, 20),
                    EmpGender = Gender.Male,
                    EmpPhone = "01234567892",
                    EmpAddress = "Maadi",
                    EmpEmail = "clinicdoctor@roshetaclinic.org",
                    EmpPassword = "Admin$123",
                    EmpNationalId = "12345678912346",
                    EmpHirigDate = new DateTime(2023, 09, 20),
                    EmpSalary = 10300.00,
                    EmployeeTypeId = 2
                },
                new Employee
                {
                    EmpId = 3,
                    EmpFirstName = "Admin",
                    EmpLastName = "Nurse",
                    EmpBirthDate = new DateTime(2023, 09, 20),
                    EmpGender = Gender.Female,
                    EmpPhone = "01234567893",
                    EmpAddress = "Maadi",
                    EmpEmail = "clinicnurse@roshetaclinic.org",
                    EmpPassword = "Admin$123",
                    EmpNationalId = "12345678912347",
                    EmpHirigDate = new DateTime(2023, 09, 20),
                    EmpSalary = 10100.00,
                    EmployeeTypeId = 3
                },
                new Employee
                {
                    EmpId = 4,
                    EmpFirstName = "Admin",
                    EmpLastName = "Receptionist",
                    EmpBirthDate = new DateTime(2023, 09, 20),
                    EmpGender = Gender.Female,
                    EmpPhone = "01234567894",
                    EmpAddress = "Maadi",
                    EmpEmail = "clinicreceptionist@roshetaclinic.org",
                    EmpPassword = "Admin$123",
                    EmpNationalId = "12345678912348",
                    EmpHirigDate = new DateTime(2023, 09, 20),
                    EmpSalary = 9900.00,
                    EmployeeTypeId = 4
                },
                new Employee
                {
                    EmpId = 5,
                    EmpFirstName = "Admin",
                    EmpLastName = "Examination",
                    EmpBirthDate = new DateTime(2023, 09, 20),
                    EmpGender = Gender.Male,
                    EmpPhone = "01234567895",
                    EmpAddress = "Maadi",
                    EmpEmail = "clinicexamination@roshetaclinic.org",
                    EmpPassword = "Admin$123",
                    EmpNationalId = "12345678912349",
                    EmpHirigDate = new DateTime(2023, 09, 20),
                    EmpSalary = 9700.00,
                    EmployeeTypeId = 5
                });

            modelBuilder.Entity<EmpDoctorSpecilzation>().HasData(
                new EmpDoctorSpecilzation 
                { 
                    EmpDoctorSpecilzationId = 1,
                    DoctorId = 2,
                    DoctorSpecilzationId = 1
                });

            modelBuilder.Entity<DoctorSchedule>().HasData(
                new DoctorSchedule
                {
                    ScheduleId = 1,
                    DoctorId = 2,
                    DaysOfWeek = (DaysOfWeek.Sunday | DaysOfWeek.Monday)
                });

            // Patient
            modelBuilder.Entity<PatientType>().HasData(
                new PatientType
                {
                    PatientTypeId = 1,
                    PatientTypeName = "Walk in"
                },
                new PatientType
                {
                    PatientTypeId = 2,
                    PatientTypeName = "Online"
                },
                new PatientType
                {
                    PatientTypeId = 3,
                    PatientTypeName = "Deleted"
                },
                new PatientType
                {
                    PatientTypeId = 4,
                    PatientTypeName = "Blocked"
                });

            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    PatientId = 1,
                    PatientFirstName = "Test",
                    PatientLastName = "Patient",
                    PatientBirthDate = new DateTime(1999, 12, 21),
                    PatientGender = Gender.Male,
                    PatientPhone = "01127616957",
                    PatientEmail = "testpatient@roshetaclinic.com",
                    PatientPassword = "Patient$123",
                    PatientTypeId = 2
                });

            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod
                {
                    PaymentMethodId = 1,
                    PaymentMethodName = "Cash"
                },
                new PaymentMethod
                {
                    PaymentMethodId = 2,
                    PaymentMethodName = "Visa"
                });

            modelBuilder.Entity<Fee>().HasData(
                new Fee
                {
                    FeeId = 1,
                    FeeName = "Booking",
                    FeeAmount = 150
                },
                new Fee
                {
                    FeeId = 2,
                    FeeName = "FollowUp",
                    FeeAmount = 100
                });

            modelBuilder.Entity<AppointmentType>().HasData(
                new AppointmentType
                {
                    AppointmentTypeId = 1,
                    TypeName = "Booking"
                },
                new AppointmentType
                {
                    AppointmentTypeId = 2,
                    TypeName = "FollowUp"
                });
                
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    AppointmentId = 1,
                    AppointmentTypeId = 1,
                    PaymentMathodId = 1,
                    PatientId = 1,
                    DoctorId = 2,
                    ExaminationComment = "Mental Health not good"
                });

            // Medicine
            modelBuilder.Entity<Medicine>().HasData(
                new Medicine
                {
                    MedicineId = 1,
                    MedicineName = "Night Calm",
                    MedicineDescription = "Take it when you want to sleep"
                },
                new Medicine
                {
                    MedicineId = 2,
                    MedicineName = "banadol",
                    MedicineDescription = "Take it when you have Headache"
                });

            modelBuilder.Entity<Dosage>().HasData(
                new Dosage
                {
                    DosageId = 1,
                    DosageName = "1/Day"
                },
                new Dosage
                {
                    DosageId = 2,
                    DosageName = "2/Day"
                });

            // Exminations Records
            modelBuilder.Entity<LaboratoryType>().HasData(
                new LaboratoryType
                {
                    LabTypeId = 1,
                    LabName = "CBC - Test"
                },
                new LaboratoryType
                {
                    LabTypeId = 2,
                    LabName = "CMP - Test"
                });

            modelBuilder.Entity<RadiologyType>().HasData(
                new RadiologyType
                {
                    RadiologyTypeId = 1,
                    RadiologyName = "CT Scan"
                },
                new RadiologyType
                {
                    RadiologyTypeId = 2,
                    RadiologyName = "MRI"
                });

            modelBuilder.Entity<RadiologyRecord>().HasData(
                new RadiologyRecord
                {
                    RadiologyRecordId = 1,
                    RadioResult = "Record01.pdf",
                    AppointmentId = 1,
                });

            modelBuilder.Entity<LaboratoryRecord>().HasData(
                new LaboratoryRecord
                {
                    LaboratoryRecordId = 1,
                    LabResult = "Record01.pdf",
                    AppointmentId = 1,
                });

            // Exminations Orders
            modelBuilder.Entity<PatientVital>().HasData(
                new PatientVital
                {
                    VitalId = 1,
                    BloodPressure = "120/180",
                    BloodType = BloodType.APositive,
                    HeartRate = 72,
                    Temperature = 35,
                    Height = 169,
                    Weight = 65,
                    PatientComment = "I'm dining doctor",
                    AppointmentId = 1,
                    NurseId = 3
                });

            modelBuilder.Entity<LaboratoryOrder>().HasData(
                new LaboratoryOrder
                {
                    LaboratoryOrderId = 1,
                    AppointmentId = 1,
                    LabTypeId = 1
                },
                new LaboratoryOrder
                {
                    LaboratoryOrderId = 2,
                    AppointmentId = 1,
                    LabTypeId = 2
                });

            modelBuilder.Entity<RadiologyOrder>().HasData(
                new RadiologyOrder
                {
                    RadiologyOrderId = 1,
                    AppointmentId = 1,
                    RadiologyTypeId = 1
                },
                new RadiologyOrder
                {
                    RadiologyOrderId = 2,
                    AppointmentId = 1,
                    RadiologyTypeId = 2
                }); ;

            modelBuilder.Entity<MedicineOrder>().HasData(
                new MedicineOrder
                {
                    MedicineOrderId = 1,
                    MedicineId = 1,
                    DosageId = 1, 
                    AppointmentId = 1,
                    Instructions = "When you feel better stop"
                });
        }
    }
}
