using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Server_WCF.Contollers
{
    /// <summary>
    /// Контроллер для отправки уведомлений по почте
    /// </summary>
    public class MailController
    {
        private readonly MailAddress serviceAddress;
        private readonly MailAddress addressToReceiveMessage;
        private readonly SmtpClient smtpClient;
        internal MailController()
        {
            serviceAddress = new MailAddress("TestmailSend@transkart.ru");
            addressToReceiveMessage = new MailAddress("TestmailReceive@transkart.ru");
            smtpClient = new SmtpClient("smtp.beget.com", 25);
            smtpClient.Credentials = new NetworkCredential(serviceAddress.Address, "X%Yr8owH");
        }
        /// <summary>
        /// Метод закоментирован тк Сервис блочит такие частые отправки сообщений 
        /// </summary>
        /// <param name="messageBody"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(string messageBody)
        {
            try
            {
                var message = new MailMessage(serviceAddress, addressToReceiveMessage);
                message.Subject = "Database added new 50 records";
                message.Body = messageBody;
                message.IsBodyHtml = true;
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(message);
                System.Console.WriteLine($"Message Sent to{addressToReceiveMessage}");
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e);
                return;
            }


        }
    }
}
