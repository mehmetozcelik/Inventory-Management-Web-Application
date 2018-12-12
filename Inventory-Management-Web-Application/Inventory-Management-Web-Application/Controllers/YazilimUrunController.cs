using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory_Management_Web_Application.Models;


namespace Inventory_Management_Web_Application.Controllers
{
    public class YazilimUrunController : Controller
    {
        InventoryContext db = new InventoryContext();
        // GET: YazilimUrun

        [HttpGet]
        public ActionResult Listesi()
        {
            ViewBag.ayarlar = db.Ayarlar.FirstOrDefault();
            return View(db.YazılımUrun.ToList());
        }

        public PartialViewResult altKategoriDropdown(int id)
        {
            var altkategoriler = db.AltKategori.Where(x => x.AnaKategorID == id).ToList();
            ViewBag.altkategoriler = new SelectList(altkategoriler, "ID", "KategoriAdi");
            return PartialView();
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            var anakategoriler = db.AnaKategori.ToList();
            ViewBag.anakategoriler = new SelectList(anakategoriler, "ID", "KategoriAdi");
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Urun u)
        {
            int Lastid = 0;
            if (db.Urun != null)
            {
                Lastid = db.Urun.Max(x => x.ID);
            }
            Lastid = db.Urun.Max(x => x.ID);
            string urunKodu = u.altKategoriID.ToString() + "1000" + DateTime.Now.Year.ToString() + (Lastid + 1).ToString();
            u.UrunKodu = urunKodu;
            db.Urun.Add(u);
            db.SaveChanges();
            return RedirectToAction("Listesi");
        }

        [HttpPost]
        public ActionResult Sil(int id)
        {
            YazılımUrun b = db.YazılımUrun.Where(x => x.ID == id).SingleOrDefault();
            if (b == null)
            {
                return Json(false);
            }
            else
            {
                try
                {
                    db.YazılımUrun.Remove(b);
                    db.SaveChanges();
                    return Json(true);
                }
                catch (Exception)
                {
                    return Json("FK");
                }

            }
        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            Urun u = db.Urun.Where(x => x.ID == id).FirstOrDefault();

            var anakategoriler = db.AltKategori.ToList();
            var urunbirimler = db.UrunBirim.ToList();
            ViewBag.anakategoriler = new SelectList(anakategoriler, "ID", "KategoriAdi");
            ViewBag.urunbirimler = new SelectList(urunbirimler, "ID", "Adi");
            if (u == null)
            {
                return RedirectToAction("Hata", "Admin");
            }
            return View(u);
        }

        [HttpPost]
        public ActionResult Guncelle(Urun u)
        {
            Urun gu = db.Urun.Where(x => x.ID == u.ID).FirstOrDefault();
            if (gu == null)
            {
                return RedirectToAction("Hata", "Admin");
            }
            gu.altKategoriID = u.altKategoriID;
            gu.UrunAdi = u.UrunAdi;
            gu.Aciklama = u.Aciklama;
            gu.StokMiktari = u.StokMiktari;
            gu.UrunBirimID = u.UrunBirimID;
            gu.UrunKodu = u.UrunKodu;
            gu.UrunSeriNo = u.UrunSeriNo;
            db.SaveChanges();
            return RedirectToAction("Listesi");
        }
    }
}