using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clinc_Care_MVC_Grad_PROJ.EmailServices
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
    }
}
