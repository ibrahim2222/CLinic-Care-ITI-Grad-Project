using Clinc_Care_MVC_Grad_PROJ.Models;
using Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Client;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client;
using Clinc_Care_MVC_Grad_PROJ.Models.RepoServices.Managment;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Managment;
using Microsoft.EntityFrameworkCore;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Clinc_Care_MVC_Grad_PROJ.EmailServices;
using Hangfire;
using Clinc_Care_MVC_Grad_PROJ.whatsappService;
using Clinc_Care_MVC_Grad_PROJ.Models.RepoServices;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Clinc_Care_MVC_Grad_PROJ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ClinicDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IInitializeDefaultData, InitializeDefaultData>();

            builder.Services.AddScoped<IPatientRepository, PatientRepoService>();
            builder.Services.AddScoped<IPatientTypeRepository, PatientTypeRepoService>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepoService>();
            builder.Services.AddScoped<IAppointmentTypeRepository, AppointmentTypeRepoService>();
            builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepoService>();
            builder.Services.AddScoped<IFeeRepository, FeeRepoService>();

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepoService>();
            builder.Services.AddScoped<IEmployeeTypeRepository, EmployeeTypeRepoService>();
            builder.Services.AddScoped<IDoctorSpecializionRepository, DoctorSpecializionRepoService>();
            builder.Services.AddScoped<IDoctorScheduleRepository, DoctorScheduleRepoService>();
            builder.Services.AddScoped<IEmpDoctorSpecializionRepository, EmpDoctorSpecializionRepoService>();

            builder.Services.AddScoped<IRadiologyTypeRepository, RadiologyTypeRepoService>();
            builder.Services.AddScoped<IRadiologyRecordRepository, RadiologyRecordRepoService>();
            builder.Services.AddScoped<ILabratoryTypeRepository, LabTypeRepoService>();
            builder.Services.AddScoped<ILaboratoryRecordRepository,LabRecordRepoService>();

            builder.Services.AddScoped<IPatientVitalRepository, PatientVitalRepoService>();
            builder.Services.AddScoped<IMedicineOrderRepository, MedicineOrderRepoService>();
            builder.Services.AddScoped<ILaboratoryOrderRepository, LaboratoryOrderRepoService>();
            builder.Services.AddScoped<IRadiologyOrderRepository, RadiologyOrderRepoService>();

            builder.Services.AddScoped<IMedicineRepository, MedicineRepoService>();
            builder.Services.AddScoped<IDosageRepository, DosageRepoService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            // Identity
            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                //opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<ClinicDbContext>().AddDefaultTokenProviders();


            // google%facebook
            builder.Services.AddAuthentication()
           .AddGoogle("google", opt =>
           {
               var googleAuth = builder.Configuration.GetSection("Authentication:Google");
               opt.ClientId = googleAuth["ClientId"];
               opt.ClientSecret = googleAuth["ClientSecret"];
               opt.SignInScheme = IdentityConstants.ExternalScheme;
           });

            var configuration = builder.Configuration;
            builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
            });

            //Authorization
            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            //token for email
            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(2));

            //emailconifg
            var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);
            builder.Services.AddScoped<IEmailSender, EmailSender>();

            //stripe
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

            //whatsapp
            builder.Services.AddScoped<ITwilioService, TwilioService>();

            //hangfire
            builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddHangfireServer();


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //hangfire
            app.UseHangfireDashboard();

            app.MapAreaControllerRoute(
             name: "ClientArea",
             areaName: "Client",
             pattern: "Client/{controller}/{action=index}/{id?}");

            app.MapAreaControllerRoute(
               name: "ManagementArea",
               areaName: "Managment",
               pattern: "Managment/{controller}/{action=index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}