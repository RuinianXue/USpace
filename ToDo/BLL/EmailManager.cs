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

        public static string SendVerificationCode(string email)
        {
            string verificationCode = GenerateVerificationCode();

            try
            {
                // Create a MailMessage instance
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(Settings.EmailFrom),
                    To = { new MailAddress(email) },
                    Subject = "【USPace】Verification Code",
                    Body = "Your verification code: " + verificationCode
                };

                // Create a SmtpClient instance
                using (SmtpClient client = new SmtpClient(Settings.SmtpClient))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(Settings.EmailFrom, Settings.EmailPwd);

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

        public static void SendNotice(string emailTo, string subject, string body)
        {
            try
            {
                // 验证电子邮件地址格式
                MailAddress mailAddress = new MailAddress(emailTo);

                // 指定发件人、收件人、邮件主题和内容
                string from = Settings.EmailFrom;

                // 创建一个 SmtpClient 对象
                SmtpClient smtpClient = new SmtpClient(Settings.SmtpClient, 587);

                // 指定发件人的用户名和密码
                smtpClient.Credentials = new NetworkCredential(Settings.EmailFrom, Settings.EmailPwd);

                // 启用安全连接
                smtpClient.EnableSsl = true;

                // 创建一个 MailMessage 对象
                MailMessage mailMessage = new MailMessage(from, emailTo, subject, body);

                // 发送邮件
                smtpClient.Send(mailMessage);
            }
            catch (FormatException ex)
            {
                // 处理格式异常，可能是因为 emailTo 不是有效的电子邮件地址格式
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
