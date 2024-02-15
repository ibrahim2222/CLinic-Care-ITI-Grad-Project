using Microsoft.Extensions.Configuration;
using Twilio;
using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Humanizer;

namespace Clinc_Care_MVC_Grad_PROJ.whatsappService
{
    public class TwilioService : ITwilioService
    {
        private readonly IConfiguration _configuration;

        public TwilioService(IConfiguration configuration)
        {
            _configuration = configuration;
            TwilioClient.Init(_configuration["Twilio:AccountSid"], _configuration["Twilio:AuthToken"]);
        }

        public async Task SendMessageAsync(string toPhoneNumber, string message)
        {
            var messageResource = await MessageResource.CreateAsync(
                body: message,
                from: new Twilio.Types.PhoneNumber("+18598982517"),
                to: new Twilio.Types.PhoneNumber(toPhoneNumber)
            );
        } 
        public async Task SendWhatsappMessageAsync(string toPhoneNumber, string message)
        {
            var messageResource = await MessageResource.CreateAsync(
                body: message,
                from: new Twilio.Types.PhoneNumber("whatsapp:+14155238886"),
                to: new Twilio.Types.PhoneNumber("whatsapp:"+toPhoneNumber)
            );
        }

       
            public string GenerateCronExpression(DateTime selectedDate)
            {
              
                DateTime startDate = selectedDate.AddDays(-1);

               
                string cronExpression = $"00 17 * {startDate.Month}-{selectedDate.Month} {startDate.DayOfWeek.ToString().Substring(0,3)}-{selectedDate.DayOfWeek.ToString().Substring(0, 3)}";

                return cronExpression;
            }
        
    }

}
