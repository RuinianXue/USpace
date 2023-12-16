using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using HandyControl.Controls;
using UIDisplay.Utils;

namespace UIDisplay.BLL
{
    public class EmailManager
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// 发送验证码邮件
        /// </summary>
        /// <param name="email">目标邮箱</param>
        /// <returns>生成的验证码</returns>
        public static async Task<string> SendVerificationCode(string email)
        {
            string verificationCode = GenerateVerificationCode();

            try
            {
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(Settings.EmailFrom),
                    To = { new MailAddress(email) },
                    Subject = "【USPace】Verification Code",
                    Body = "Your verification code: " + verificationCode
                };

                using (SmtpClient client = new SmtpClient(Settings.SmtpClient))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(Settings.EmailFrom, Settings.EmailPwd);

                    await client.SendMailAsync(mailMessage);
                }

                return verificationCode;
            }
            catch (SmtpException ex)
            {
                Growl.Error($"Failed to send email: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 发送通知邮件
        /// </summary>
        /// <param name="emailTo">目标邮箱</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        public static void SendNotice(string emailTo, string subject, string body)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(emailTo);

                string from = Settings.EmailFrom;

                SmtpClient smtpClient = new SmtpClient(Settings.SmtpClient, 587);

                smtpClient.Credentials = new NetworkCredential(Settings.EmailFrom, Settings.EmailPwd);

                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage(from, emailTo, subject, body);

                smtpClient.Send(mailMessage);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Invalid email address format: " + emailTo);
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static string GenerateVerificationCode()
        {
            return Convert.ToString(random.Next(100000, 999999));
        }
    }
}
