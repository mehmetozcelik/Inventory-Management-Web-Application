using Inventory_Management_Web_Application.Models;
using Inventory_Management_Web_Application.ReportFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                urunler.AddRange(db.Urun.Where(x => x.altKategoriID == item.ID && x.Aktif==true));
            }
            return urunler;
        }

        public static List<YazilimUrun> IzinliYazilimUrunler()
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
            List<YazilimUrun> urunler = new List<YazilimUrun>();
            foreach (AltKategori item in alt)
            {
                urunler.AddRange(db.YazilimUrun.Where(x => x.altKategoriID == item.ID && x.Aktif == true));
            }
            return urunler;
        }

    }
}