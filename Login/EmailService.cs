using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace LoginTest
{
    public class EmailService
    {
        //string verificationCode;
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
                Console.WriteLine($"Error sending email: {ex.Message}");
                return null;
            }

            //实例化一个发送邮件类。
            //MailMessage mailMessage = new MailMessage();
            ////发件人邮箱地址，方法重载不同，可以根据需求自行选择
            //mailMessage.From = new MailAddress("2066892296@qq.com");
            ////收件人邮箱地址
            //mailMessage.To.Add(new MailAddress(email));//不能为空，或更改为邮箱
            ////邮件标题
            //mailMessage.Subject = "身份验证";
            //verificationCode = GenerateVerificationCode();
            ////邮件内容
            //mailMessage.Body = "这里是你的验证码：" + verificationCode;
            ////实例化一个SmtpClient类
            //SmtpClient client = new SmtpClient();
            ////在这里我使用的是qq邮箱，所以是smtp.qq.com，如果你使用的是126邮箱，那么就是smtp.126.com
            //client.Host = "smtp.qq.com";
            ////使用安全加密连接
            //client.EnableSsl = true;
            ////不和请求一块发送
            //client.UseDefaultCredentials = false;
            ////验证发件人身份(发件人的邮箱，邮箱里的生成授权码)
            //client.Credentials = new NetworkCredential("2066892296@qq.com", "nrzayyqhvyiccjae");
            ////发送
            //client.Send(mailMessage);//在QQ邮箱内开启IMAP/SMTP服务
            ////MessageBox.Show("已成功发送。请等待约3秒以获取验证码！", "提示");
            //return verificationCode;
        }

        private static string GenerateVerificationCode()
        {
            //Random rd = new Random();
            //label3.Text = Convert.ToString(rd.Next(100000, 999999));
            return Convert.ToString(random.Next(100000, 999999));
        }
    }
}
