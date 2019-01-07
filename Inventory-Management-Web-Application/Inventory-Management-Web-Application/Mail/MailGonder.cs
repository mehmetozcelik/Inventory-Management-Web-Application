using Inventory_Management_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory_Management_Web_Application.Mail
{
    public class MailGonder
    {
        public MailGonder(string ToMail, string Subject, string Body)
        {
            Mail.mailUserName = "Sistem Mail Adresi";
            Mail.mailPassword = "Sistem Mail Şifresi";

            Mail mailer = new Mail();
            mailer.ToEmail = ToMail;
            mailer.Subject = Subject;
            mailer.Body = Body;
            mailer.IsHtml = true;
            mailer.Send();
        }

        //----------------- Mail İçerikleri
        public static string sifreYenileme(Personel personel)
        {
            return "<h2 style='color: #2e6c80;'>BI Stok</h2>" +
            "<p> Düzce Üniversitesi Bilgi İşlem Daire Başkanlığı</p>" +
            "<p>Sayın " + personel.Adi + " " + personel.Soyadi + " ;.</p>" +
            "<p>Şifreniz başarılı bir şekilde yenilenmiştir. Yeni Şireniz Aşağıdaki gibidir.</p>" +
            "<p><strong>Yeni Şifre :&nbsp;</strong>" + personel.Sifre.ToString() + "" +
            "<p><strong>Sisteme giriş yaptıktan sonra kişisel Bilgilerim alanından şifrenizi güncelleyebilirsiniz.</strong></p>";
        }
    }
}