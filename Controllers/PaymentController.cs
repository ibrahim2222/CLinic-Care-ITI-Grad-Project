using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Clinc_Care_MVC_Grad_PROJ.Models;
using Stripe;
using System.Security.Claims;
using Stripe.Checkout;
using Clinc_Care_MVC_Grad_PROJ.EmailServices;
using Clinc_Care_MVC_Grad_PROJ.Models.Repositories.Client;
using Clinc_Care_MVC_Grad_PROJ.Areas.Client.Models;

using Newtonsoft.Json;
using Clinc_Care_MVC_Grad_PROJ.whatsappService;
using Hangfire;
using NETCore.MailKit.Core;
using Clinc_Care_MVC_Grad_PROJ.Models.ViewModels;
using Clinc_Care_MVC_Grad_PROJ.Models.UnitOfWork;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Resturant_RES_MVC_ITI_PRJ.Controllers
{
    [Authorize(Roles = "Client")]

    public class PaymentController : Controller
    {
        private readonly StripeSettings _stripeSettings;

        public IEmailSender EmailSender { get; }
        public ITwilioService TwilioService { get; }
        public IConfiguration Configuration { get; }
        public IUnitOfWork UnitOfWork { get; }

        public PaymentController(IOptions<StripeSettings> stripeSettings,
            IEmailSender emailSender,
            ITwilioService twilioService,
            IConfiguration configuration,
            IUnitOfWork unitOfWork
          )
        {
            _stripeSettings = stripeSettings.Value;
            EmailSender = emailSender;
            TwilioService = twilioService;
            Configuration = configuration;
            UnitOfWork = unitOfWork;
            ;
        }

     

        public IActionResult CreateCheckoutSession(AppoRegVm appoRegVm)
        {
            StringBuilder appDesc = new StringBuilder();
            appDesc.Append(appoRegVm.AppoitmentDate.ToString());
            appDesc.Append(" - ");
            appDesc.Append(UnitOfWork.DoctorSpecializionRepository.GetDoctorSpecializionById(appoRegVm.SpecializationID).SpecializationName);


            Appointment app = new Appointment()
            {
                PaymentMathodId = appoRegVm.IsCash,
                PatientId = UnitOfWork.PatientRepository.GetAllPatients().SingleOrDefault(p => p.PatientEmail == User.FindFirst(ClaimTypes.Name)?.Value).PatientId,
                DoctorId = UnitOfWork.EmpDoctorSpecializionRepository.GetAllEmpDoctorSpecilzations().Where(eds => eds.DoctorSpecilzationId == appoRegVm.SpecializationID).FirstOrDefault().DoctorId,
                IsCanceled = false,
                ExaminationComment = null,
                IsPaid = false,
                AppointmentState = AppointmentStates.reserverd,
                AppointmentTypeId = 1,
                AppointmentDate = appoRegVm.AppoitmentDate

            };

            UnitOfWork.AppointmentRepository.InsertAppointment(app);

            var appId = app.AppointmentId;



            var currency = "usd";
            var successUrl = $"https://localhost:7150/Payment/success?AppId={appId}";
            var cancelUrl = $"https://localhost:7150/Payment/cancel?AppId={appId}";
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            Console.WriteLine(appoRegVm.Fees);
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            UnitAmount = Convert.ToInt32(appoRegVm.Fees) * 100,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name ="Appointment",
                                Description = appDesc.ToString(),
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };


            var service = new SessionService();
            var session = service.Create(options);


            var routeValues = new RouteValueDictionary(new { AppId = appId });

            if (appoRegVm.IsCash == 1)
            {
                return RedirectToAction("success", "Payment", routeValues);
            }

            return Redirect(session.Url);

        }


        public async Task<IActionResult> success(int AppId)
        {
            var MyApp = UnitOfWork.AppointmentRepository.GetAppointmentById(AppId);

            if (MyApp.PaymentMathodId == 2)
            {
                MyApp.IsPaid = true;
                UnitOfWork.AppointmentRepository.UpdateAppointment(AppId, MyApp);
            }


            try
            {
                var fees = UnitOfWork.FeeRepository.GetAllFees().Where(f => f.FeeName == "Booking").FirstOrDefault().FeeAmount;
                var SpciName = MyApp.DoctorIdNavigation.EmpDoctorSpecilzations.FirstOrDefault().DoctorSpecilzationIdNavigation.SpecializationName;
                var DrName = UnitOfWork.EmpDoctorSpecializionRepository.GetAllEmpDoctorSpecilzations().Where(ds => ds.DoctorSpecilzationIdNavigation.SpecializationName == SpciName).FirstOrDefault().DoctorIdNavigation.EmployeeFullName;
                var AppDate = MyApp.AppointmentDate.ToString("yyyy-MM-dd");
                var message = new Message(new string[] { User.Identity.Name }, "CLinic Appointment Confiramtion Mail", $"<h3>Thank you for the Appointment For {SpciName} with Dr/ {DrName} on {AppDate}</h3></br><p>Total Fees: {fees}</p>", null);
                await EmailSender.SendEmailAsync(message);

                //await TwilioService.SendMessageAsync("+201127616957", $"hello , Your appointment for {SpciName} is coming up on {AppDate}");
                await TwilioService.SendWhatsappMessageAsync("+201127616957", $"hello , Your appointment for {SpciName} is coming up on {AppDate}");
                var cron = TwilioService.GenerateCronExpression(DateTime.Now);
                RecurringJob.AddOrUpdate(() => new TwilioService(Configuration).SendWhatsappMessageAsync("+201142601607", $"hello , Your appointment for {SpciName} is coming up on {AppDate}"), cron);
            }
            catch (Exception ex)
            {
                return View("error");
            }

            return View("PaymentSuccess");
        }

        public IActionResult cancel(int AppId)
        {
            UnitOfWork.AppointmentRepository.DeleteAppointment(AppId, true);
            return View("PaymentFailed");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("PaymentFailed");
        }

        public IActionResult stop(int OrderID)
        {
            RecurringJob.RemoveIfExists("TwilioService.SendWhatsappMessageAsync");
            return Content("stoped");
        }

    }
}
