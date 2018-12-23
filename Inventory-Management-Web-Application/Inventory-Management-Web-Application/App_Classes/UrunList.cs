using Inventory_Management_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory_Management_Web_Application.App_Classes
{
    public class UrunList
    { 
        public static List<Urun> IzinliUrunler()
        {
            InventoryContext db = new InventoryContext();
            Personel p = (Personel)HttpContext.Current.Session["Kullanici"];
            List<KategoriRol> kt = db.KategoriRol.Where(x => x.RolID == p.RolID).ToList();
            List<AltKategori> alt = new List<AltKategori>();
            foreach (KategoriRol item in kt)
            {
                AltKategori k = db.AltKategori.Where(x => x.ID == item.KategoriID).SingleOrDefault();
                alt.Add(k);
            }
            List<Urun> urunler = new List<Urun>();
            foreach (AltKategori item in alt)
            {
                urunler.AddRange(db.Urun.Where(x => x.altKategoriID == item.ID));
            }
            return urunler;
        }

        public static List<YazılımUrun> IzinliYazilimUrunler()
        {
            InventoryContext db = new InventoryContext();
            Personel p = (Personel)HttpContext.Current.Session["Kullanici"];
            List<KategoriRol> kt = db.KategoriRol.Where(x => x.RolID == p.RolID).ToList();
            List<AltKategori> alt = new List<AltKategori>();
            foreach (KategoriRol item in kt)
            {
                AltKategori k = db.AltKategori.Where(x => x.ID == item.KategoriID).SingleOrDefault();
                alt.Add(k);
            }
            List<YazılımUrun> urunler = new List<YazılımUrun>();
            foreach (AltKategori item in alt)
            {
                urunler.AddRange(db.YazılımUrun.Where(x => x.altKategoriID == item.ID));
            }
            return urunler;
        }
    }
}