using Inventory_Management_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory_Management_Web_Application.Controllers
{
    public class TedarikciController : Controller
    {
        // GET: Tedarikci
        InventoryContext db = new InventoryContext();

        [HttpGet]
        public ActionResult Listesi()
        {
            return View(db.Tedarikci.ToList());
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Tedarikci u)
        {
            db.Tedarikci.Add(u);
            db.SaveChanges();
            return RedirectToAction("Listesi");
        }

        [HttpPost]
        public ActionResult Sil(int id)
        {
            Tedarikci b = db.Tedarikci.Where(x => x.ID == id).SingleOrDefault();
            if (b == null)
            {
                return Json(false);
            }
            else
            {
                try
                {
                    db.Tedarikci.Remove(b);
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
            Tedarikci u = db.Tedarikci.Where(x => x.ID == id).FirstOrDefault();
            if (u == null)
            {
                return RedirectToAction("Hata", "Admin");
            }
            return View(u);
        }

        [HttpPost]
        public ActionResult Guncelle(Tedarikci u)
        {
            Tedarikci gu = db.Tedarikci.Where(x => x.ID == u.ID).FirstOrDefault();
            if (gu == null)
            {
                return RedirectToAction("Hata", "Admin");
            }
          
            gu.FirmaAdi = u.FirmaAdi;
            gu.FirmaTel = u.FirmaTel;
            gu.FirmaAdres = u.FirmaAdres;
            gu.FirmaMail = u.FirmaMail;
            gu.YetkiliAdi = u.YetkiliAdi;
            gu.YetkiliSoyadi = u.YetkiliSoyadi;
            gu.YetkiliUnvani = u.YetkiliUnvani;
            gu.YetkiliTel = u.YetkiliTel;
            gu.YetkiliMail = u.YetkiliMail;
            db.SaveChanges();
            return RedirectToAction("Listesi");
        }


    }
}