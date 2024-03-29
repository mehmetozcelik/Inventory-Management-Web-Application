﻿using Inventory_Management_Web_Application.App_Classes;
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
                List<Urun> stok = UrunList.IzinliUrunler();//db.Urun.Where(x => x.Aktif == true).ToList();
                List<Urun> temp = new List<Urun>();
                foreach (Urun item in stok)
                {
                    if (item.UrunStok.Where(x => x.UrunID == item.ID).ToList().Count <= ayar.UrunStok)
                    {
                        temp.Add(item);
                    }
                }
                // Stoğa azalan Yazılımlar
                List<YazilimUrun> Ystok = UrunList.IzinliYazilimUrunler();//db.YazilimUrun.ToList();
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

                List <Personel> PersonelStokBulten = db.Personel.Where(x => x.StokBulten == true).ToList(); 
                foreach (Personel mail in PersonelStokBulten)
                {
                    MailGonder m = new MailGonder(mail.Email, "BISTOK - Ürün Stok Bilgilendirmesi", stokBody);
                }
            }

            catch (Exception)
            {
                //HttpContext.Current.Response.Redirect("/Admin/Hata");
            }




        }
    }
}