using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory_Management_Web_Application.Models;

namespace Inventory_Management_Web_Application.Controllers
{
    public class PersonelController : Controller
    {
        public InventoryContext db = new InventoryContext();
        // GET: Personel
        public ActionResult TaListesi()
        {
            var TeslimAlanPersonel = db.TeslimAlanPersonel.ToList();
            ViewBag.TeslimAlanPersonel = TeslimAlanPersonel;
            return View();
        }

        public ActionResult TaEkle()
        {
            ViewBag.Birimler = db.Birim.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult TaEkle(TeslimAlanPersonel veri)
        {
            db.TeslimAlanPersonel.Add(veri);
            db.SaveChanges();
            return RedirectToAction("TaListesi");
        }

        public ActionResult TaDuzenle(int ID)
        {
            ViewBag.TeslimAlanPersonel = db.TeslimAlanPersonel.SingleOrDefault(x => x.ID == ID);
            ViewBag.Birimler = db.Birim.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult TaDuzenle(TeslimAlanPersonel veri)
        {
            db.Entry(veri).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TaListesi");
        }

        [HttpPost]
        public ActionResult TaSil(int id)
        {
            TeslimAlanPersonel b = db.TeslimAlanPersonel.Where(x => x.ID == id).SingleOrDefault();
            if (b == null)
            {
                return Json(false);
            }
            else
            {
                db.TeslimAlanPersonel.Remove(b);
                db.SaveChanges();
                return Json(true);
            }
        }
    }
}