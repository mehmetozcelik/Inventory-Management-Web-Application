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
            Mail.mailUserName = "hamza.tas610@gmail.com";
            Mail.mailPassword = "b.1411230131B";

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


        public static string urunStokBildirim(List<Urun> urun, List<YazilimUrun> Yurun, List<YazilimUrun> YLisansUrun)
        {
            string stok = "";
            string ystok = "";
            string ylis = "";

            foreach (Urun item in urun)
            {
                stok = stok + "<li>" + item.UrunAdi + " ( " + item.UrunStok.Where(x => x.UrunID == item.ID).ToList().Count + " " + item.UrunBirim.Adi + " )</li> ";
            }

            foreach (YazilimUrun item in Yurun)
            {
                ystok = ystok + "<li>" + item.UrunAdi + " ( " + item.KeyAdet + " ) </li> ";
            }

            foreach (YazilimUrun item in Yurun)
            {
                ylis = ylis + "<li>" + item.UrunAdi + " ( " + (item.LisansBitisTarihi.Value.Date - DateTime.Now.Date).TotalDays + " Gün Kaldı.) </li> ";
            }

            if (stok=="")
            {
                stok = "Bulunmamaktadır";
            }
            if (ystok == "")
            {
                ystok = "Bulunmamaktadır";
            }
            if (ylis == "")
            {
                ylis = "Bulunmamaktadır";
            }

            return "<h1 style='text-align:center;'><span style='color:#008080;'><strong>BI STOK</strong></span></h1>" +
            "<h5 style='text-align:center;'>Düzce Üniversitesi Bilgi İşlem Daire Başkanlığı /  Ürün Stok Bilgilendirmesi</h5>" +

            "<p><strong>Stok Adeti Azalan Ürünlerin Listesi</strong></p>"
            + "<ul>"
            + stok
            + "</ul>" +

            "<p>&nbsp;</p>" +

            "<p><strong>Key Adeti Azalan Yazılım Ürünlerinin Listesi</strong></p>"
            + "<ul>"
            + ystok
            + "</ul>" +

            "<p>&nbsp;</p>" +

            "<p><strong>Lisans Bitiş Tarihi Yaklaşan Yazılım Ürünlerinin Listesi</strong></p>"
            + "<ul>"
            + ylis
            + "</ul>";
        }


    }
}