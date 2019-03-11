using Inventory_Management_Web_Application.Mail;
using Inventory_Management_Web_Application.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
namespace Inventory_Management_Web_Application.Jobs
{
    public class MailJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                InventoryContext db = new InventoryContext();
                Ayarlar ayar = db.Ayarlar.FirstOrDefault();

                // Stoğa azalan Ürünler
                List<Urun> stok = db.Urun.Where(x => x.Aktif == true).ToList();
                List<Urun> temp = new List<Urun>();
                foreach (Urun item in stok)
                {
                    if (item.UrunStok.Where(x => x.UrunID == item.ID).ToList().Count <= ayar.UrunStok)
                    {
                        temp.Add(item);
                    }
                }
                // Stoğa azalan Yazılımlar
                List<YazilimUrun> Ystok = db.YazilimUrun.ToList();
                List<YazilimUrun> YstokTemp = new List<YazilimUrun>();
                List<YazilimUrun> YLisansTemp = new List<YazilimUrun>();

                foreach (YazilimUrun yzlm in Ystok)
                {
                    if (yzlm.KeyAdet <= ayar.YazilimUrunStok)
                    {
                        YstokTemp.Add(yzlm);
                    }
                    if ((yzlm.LisansBitisTarihi.Value.Date - DateTime.Now.Date).TotalDays <= ayar.YazilimUrun)
                    {
                        YLisansTemp.Add(yzlm);
                    }
                }
                // Lisans bitişi azalan Yazılımlar

                string stokBody = MailGonder.urunStokBildirim(temp, YstokTemp, YLisansTemp);
                // Atılacak mailler
                string mailler = ayar.UserBilgiMail.ToString();
                string[] Menuparts = mailler.Split(';');
                Array.Reverse(Menuparts);
                foreach (string mail in Menuparts)
                {
                    MailGonder m = new MailGonder(mail, "BISTOK - Ürün Stok Bilgilendirmesi", stokBody);
                }
            }

            catch (Exception)
            {
                //HttpContext.Current.Response.Redirect("/Admin/Hata");
            }




        }
    }
}