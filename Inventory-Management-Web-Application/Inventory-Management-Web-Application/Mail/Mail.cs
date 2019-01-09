using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Inventory_Management_Web_Application.Mail
{
    public class Mail
    {
        // Mail Ayarları
        public static string mailUserName { get; set; }
        public static string mailPassword { get; set; }
        public static string mailHost { get; set; }
        public static int mailPort { get; set; }
        public static bool mailSSL { get; set; }

        // Mail Bilgileri
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        static Mail()
        {
            mailHost = "smtp.gmail.com";
            mailPort = 25; // Gmail 25 portunu kullan., 465 & 587; ÇAlışmadığı zaman bu portlarda denenebilir..
            mailSSL = true;
        }

        public void Send()
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = mailHost;
                smtp.Port = mailPort;
                smtp.EnableSsl = mailSSL;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(mailUserName, mailPassword);

                using (var message = new MailMessage(mailUserName, ToEmail))
                {
                    message.Subject = Subject;
                    message.Body = Body;
                    message.IsBodyHtml = IsHtml;
                    smtp.Send(message);
                }
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("/Admin/Hata");
            }

        }
    }
}