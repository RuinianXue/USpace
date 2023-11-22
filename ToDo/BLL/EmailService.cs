using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using HandyControl.Controls;

namespace UIDisplay.BLL
{
    public class EmailManager 
    { 
        private static readonly Random random = new Random();

        public static string SendVerificationCode(string email)
        {
            string verificationCode = GenerateVerificationCode();

            try
            {
                // Create a MailMessage instance
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("2066892296@qq.com"),
                    To = { new MailAddress(email) },
                    Subject = "Verification Code",
                    Body = "Your verification code: " + verificationCode
                };

                // Create a SmtpClient instance
                using (SmtpClient client = new SmtpClient("smtp.qq.com"))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("2066892296@qq.com", "nrzayyqhvyiccjae");

                    // Send the email
                    client.Send(mailMessage);
                }

                // Return the verification code for further processing if needed
                return verificationCode;
            }
            catch (SmtpException ex)
            {
                // Handle the exception (log, display an error, etc.)
                Growl.Error($"Error sending email: {ex.Message}");
                return null;
            }
        }

        private static string GenerateVerificationCode()
        {
            return Convert.ToString(random.Next(100000, 999999));
        }
    }
}
