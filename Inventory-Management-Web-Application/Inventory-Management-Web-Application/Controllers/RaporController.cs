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
            //int? altKategoriID, int? stok, DateTime? tarih , int? PersonelID, int? TedarikciID
            //List<Urun> rapor = db.Urun.Where(x => x.altKategoriID == altKategoriID || x.StokMiktari <= stok || x.EklenmeTarihi>=tarih || x.PersonelID==PersonelID || x.TedarikciID==TedarikciID).ToList();
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
        public ActionResult UrunCikis(int? altKategoriID, int? stok, DateTime? tarih, int? PersonelID)
        {
            List<UrunCikis> rapor = db.UrunCikis.Where(x =>x.Urun.altKategoriID == altKategoriID || x.CikanMictar <= stok || x.TeslimTarihi <= tarih || x.Personel.ID == PersonelID || x.UrunID!=null).ToList();
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
        public ActionResult YazilimUrun(int? altKategoriID, int? stok, DateTime? tarih, DateTime? tarihbaslangic, DateTime? tarihbitis, int? PersonelID ,int? TedarikciID)
        {
            List<YazılımUrun> rapor = db.YazılımUrun.Where(x => x.altKategoriID == altKategoriID || x.KeyAdet <= stok || x.EklenmeTarihi >= tarih || x.PersonelID == PersonelID || x.TedarikciID==TedarikciID).ToList();
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
        public ActionResult CikanYazilimUrun(int? altKategoriID, int? stok, DateTime? tarih, int? PersonelID)
        {
            List<UrunCikis> rapor = db.UrunCikis.Where(x =>x.YazilimUrunID !=null|| x.Urun.altKategoriID == altKategoriID || x.CikanMictar <= stok || x.TeslimTarihi <= tarih || x.Personel.ID == PersonelID).ToList();
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