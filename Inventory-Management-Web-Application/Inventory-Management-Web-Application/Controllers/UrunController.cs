using Inventory_Management_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory_Management_Web_Application.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        InventoryContext db = new InventoryContext();

        [HttpGet]
        public ActionResult Listesi()
        {
            return View(db.Urun.ToList());
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            var kategoriler = db.Kategori.ToList();
            var urunbirimler = db.UrunBirim.ToList();
            ViewBag.kategoriler = new SelectList(kategoriler, "ID", "KategoriAdi");
            ViewBag.urunbirimler = new SelectList(urunbirimler, "ID", "Adi");
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Urun u)
        {
            db.Urun.Add(u);
            db.SaveChanges();
            return RedirectToAction("Listesi");
        }

        [HttpPost]
        public ActionResult Sil(int id)
        {
            Urun b = db.Urun.Where(x => x.ID == id).SingleOrDefault();
            if (b == null)
            {
                return Json(false);
            }
            else
            {
                try
                {
                    db.Urun.Remove(b);
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
            var kategoriler = db.Kategori.ToList();
            var urunbirimler = db.UrunBirim.ToList();
            ViewBag.kategoriler = new SelectList(kategoriler, "ID", "KategoriAdi");
            ViewBag.urunbirimler = new SelectList(urunbirimler, "ID", "Adi");
            if (u==null)
            {
                return RedirectToAction("Hata", "Admin");
            }
            return View(u);
        }

        [HttpPost]
        public ActionResult Guncelle(Urun u)
        {
            Urun gu = db.Urun.Where(x => x.ID == u.ID).FirstOrDefault();
            if (gu==null)
            {
                return RedirectToAction("Hata", "Admin");
            }
            gu.KategoriID = u.KategoriID;
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