namespace Clinc_Care_MVC_Grad_PROJ.whatsappService
{
    public interface ITwilioService
    {
        Task SendMessageAsync(string toPhoneNumber, string message);
        Task SendWhatsappMessageAsync(string toPhoneNumber, string message);
        public string GenerateCronExpression(DateTime selectedDate);

    }
}
