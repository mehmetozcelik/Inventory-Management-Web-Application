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
        public ActionResult TeslimAlanPersonelListele()
        {
            var TeslimAlanPersonel = db.TeslimAlanPersonel.ToList();
            ViewBag.TeslimAlanPersonel = TeslimAlanPersonel;
            return View();
        }

        public ActionResult TeslimAlanPersonelEkle()
        {
            ViewBag.Birimler = db.Birim.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult TeslimAlanPersonelEkle(TeslimAlanPersonel veri)
        {
            db.TeslimAlanPersonel.Add(veri);
            db.SaveChanges();
            return RedirectToAction("TeslimAlanPersonelListele");
        }

        public ActionResult TeslimAlanPersonelDuzenle(int ID)
        {
            ViewBag.TeslimAlanPersonel = db.TeslimAlanPersonel.SingleOrDefault(x => x.ID == ID);
            ViewBag.Birimler = db.Birim.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult TeslimAlanPersonelDuzenle(TeslimAlanPersonel veri)
        {
            db.Entry(veri).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TeslimAlanPersonelListele");
        }

    }
}