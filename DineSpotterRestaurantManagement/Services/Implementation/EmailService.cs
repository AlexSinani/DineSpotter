//using RestaurantReservation.Services.Interface;
//using System.Net.Mail;
//using System.Net;

//namespace RestaurantReservation.Services.Implementation
//{
//    public class EmailService : IEmailService
//    {
//        private readonly string _smtpServer;
//        private readonly int _smtpPort;
//        private readonly string _smtpUsername;
//        private readonly string _smtpPassword;

//        public EmailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
//        {
//            _smtpServer = smtpServer;
//            _smtpPort = smtpPort;
//            _smtpUsername = smtpUsername;
//            _smtpPassword = smtpPassword;
//        }

//        public async Task SendEmailAsync(string emailAddress, string subject, string message)
//        {
//            using (var client = new SmtpClient(_smtpServer, _smtpPort))
//            {
//                client.UseDefaultCredentials = false;
//                client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
//                client.EnableSsl = true;

//                var mailMessage = new MailMessage
//                {
//                    From = new MailAddress(_smtpUsername),
//                    Subject = subject,
//                    Body = message,
//                    IsBodyHtml = true
//                };

//                mailMessage.To.Add(emailAddress);

//                try
//                {
//                    await client.SendMailAsync(mailMessage);
//                }
//                catch (Exception ex)
//                {
//                    // Handle exception (log or rethrow)
//                    throw new Exception("Failed to send email. " + ex.Message);
//                }
//            }
//        }
//    }
//}