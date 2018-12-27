using Inventory_Management_Web_Application.App_Classes;
using Inventory_Management_Web_Application.Models;
using Inventory_Management_Web_Application.ReportFilters;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory_Management_Web_Application.Controllers
{
    public class RaporController : Controller
    {
        // GET: Rapor
        InventoryContext db = new InventoryContext();

        // Ürün Raporu
        [HttpGet]
        public ActionResult Urun()
        {
            var anakategoriler = db.AnaKategori.ToList();
            ViewBag.anakategoriler = new SelectList(anakategoriler, "ID", "KategoriAdi");

            var urunbirimler = db.UrunBirim.ToList();
            ViewBag.urunbirimler = new SelectList(urunbirimler, "ID", "Adi");

            var tedarikciler = db.Tedarikci.Select(x => new
            {
                ID = x.ID,
                TedarikciAdi = x.FirmaAdi
            });
            var personeller = db.Personel.Select(x => new
            {
                ID = x.ID,
                adiSoyadi = x.Adi + " " + x.Soyadi
            });
            ViewBag.tedarikciler = new SelectList(tedarikciler, "ID", "TedarikciAdi");
            ViewBag.personeller = new SelectList(personeller, "ID", "adiSoyadi");
            return View();
        }

        [HttpPost]
        public ActionResult Urun(UrunFilter list)
        {
           List<Urun> rapor = UrunFilter.UrunSorgu(list);
            var report = new ViewAsPdf("Urun_Print", rapor)
            { };

            return report;
        }

        public ActionResult Urun_Print()
        {
            return View();
        }

        //Ürün çıkış raporu
        [HttpGet]
        public ActionResult UrunCikis()
        {
            var urunler = UrunList.IzinliUrunler();
            ViewBag.urunler = new SelectList(urunler, "ID", "UrunAdi");

            var urunbirimler = db.UrunBirim.ToList();
            ViewBag.urunbirimler = new SelectList(urunbirimler, "ID", "Adi");

          
            var personeller = db.Personel.Select(x => new
            {
                ID = x.ID,
                adiSoyadi = x.Adi + " " + x.Soyadi
            });
            ViewBag.personeller = new SelectList(personeller, "ID", "adiSoyadi");
            return View();
        }

        [HttpPost]
        public ActionResult UrunCikis(UrunCikisFilter urunCikisFilter)
        {
            List<UrunCikis> rapor = UrunCikisFilter.UrunSorgu(urunCikisFilter);
            var report = new ViewAsPdf("UrunCikis_Print", rapor)
            { };
            return report;
        }

        public ActionResult UrunCikis_Print()
        {
            return View();
        }

        //Yazılım Ürün raporu
        [HttpGet]
        public ActionResult YazilimUrun()
        {
            var anakategoriler = db.AnaKategori.ToList();
            ViewBag.anakategoriler = new SelectList(anakategoriler, "ID", "KategoriAdi");

            var urunbirimler = db.UrunBirim.ToList();
            ViewBag.urunbirimler = new SelectList(urunbirimler, "ID", "Adi");

            var tedarikciler = db.Tedarikci.Select(x => new
            {
                ID = x.ID,
                TedarikciAdi = x.FirmaAdi
            });
            var personeller = db.Personel.Select(x => new
            {
                ID = x.ID,
                adiSoyadi = x.Adi + " " + x.Soyadi
            });
            ViewBag.tedarikciler = new SelectList(tedarikciler, "ID", "TedarikciAdi");
            ViewBag.personeller = new SelectList(personeller, "ID", "adiSoyadi");
            return View();
        }

        [HttpPost]
        public ActionResult YazilimUrun(YazilimUrunFilter yazilimUrunFilter)
        {
            List<YazilimUrun> rapor = YazilimUrunFilter.UrunSorgu(yazilimUrunFilter);
            var report = new ViewAsPdf("YazilimUrun_Print", rapor)
            { };
            return report;
        }

        public ActionResult YazilimUrun_Print()
        {
            return View();
        }

        //Yazılım Ürün çıkış raporu
        [HttpGet]
        public ActionResult CikanYazilimUrun()
        {
            var urunler = UrunList.IzinliUrunler();
            ViewBag.urunler = new SelectList(urunler, "ID", "UrunAdi");

            var tedarikciler = db.Tedarikci.Select(x => new
            {
                ID = x.ID,
                TedarikciAdi = x.FirmaAdi
            });
            var personeller = db.Personel.Select(x => new
            {
                ID = x.ID,
                adiSoyadi = x.Adi + " " + x.Soyadi
            });
            ViewBag.tedarikciler = new SelectList(tedarikciler, "ID", "TedarikciAdi");
            ViewBag.personeller = new SelectList(personeller, "ID", "adiSoyadi");
            return View();
        }

        [HttpPost]
        public ActionResult CikanYazilimUrun(CikanYazilimUrunFilter yazilimUrunFilter)
        {
            List<UrunCikis> rapor = CikanYazilimUrunFilter.UrunSorgu(yazilimUrunFilter);
            var report = new ViewAsPdf("CikanYazilimUrun_Print", rapor)
            { };
            return report;
        }

        public ActionResult CikanYazilimUrun_Print()
        {
            return View();
        }



    }
}