using Inventory_Management_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory_Management_Web_Application.Controllers
{
    public class TanimController : Controller
    {
        // GET: Tanim
        InventoryContext db = new InventoryContext();
        public ActionResult TeslimBirim()
        {
            return View(db.Birim.ToList());
        }

        [HttpPost]
        public ActionResult TeslimBirimEkle(Birim b)
        {
            db.Birim.Add(b);
            db.SaveChanges();
            return RedirectToAction("TeslimBirim");
        }

        public ActionResult teslimBirimSil(int id)
        {
            Birim b = db.Birim.Where(x => x.ID == id).SingleOrDefault();
            if (b == null)
            {
                return Json(false);
            }
            else
            {
                db.Birim.Remove(b);
                db.SaveChanges();
                return Json(true);
            }
        }

        [HttpGet]
        public ActionResult TeslimBirimDuzenle(int id)
        {
            Birim b = db.Birim.Where(x => x.ID == id).FirstOrDefault();
            return View(b);
        }

        [HttpPost]
        public ActionResult TeslimBirimDuzenle(Birim b)
        {
            Birim bb = db.Birim.Where(x => x.ID == b.ID).SingleOrDefault();
            if (bb!=null)
            {
                bb.Adi = b.Adi;
                db.SaveChanges();
                return RedirectToAction("TeslimBirim");
            }
            else
            {

                return RedirectToAction("TeslimBirim");
            }
        }
    }
}