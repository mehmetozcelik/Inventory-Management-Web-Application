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


        // ---------------------------------------- Teslim birimi Tanımı -------------------------------------- //
        public ActionResult TeslimBirim()
        {
            return View(db.Birim.ToList());
        }

        [HttpPost]
        public ActionResult TeslimBirimEkle(Birim b)
        {
            try
            {
                db.Birim.Add(b);
                db.SaveChanges();
                TempData["GenelMesaj"] = "Teslim birimi ekleme işlemi başarılı bir şekilde tamamlanmıştır.";
                return RedirectToAction("TeslimBirim");
            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }

        }

        [HttpPost]
        public ActionResult teslimBirimSil(int id)
        {
            Birim b = db.Birim.Where(x => x.ID == id).SingleOrDefault();
            if (b == null)
            {
                return Json(false);
            }
            else
            {
                try
                {
                    db.Birim.Remove(b);
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
        public ActionResult TeslimBirimDuzenle(int id)
        {
            Birim b = db.Birim.Where(x => x.ID == id).FirstOrDefault();
            return View(b);
        }

        [HttpPost]
        public ActionResult TeslimBirimDuzenle(Birim b)
        {
            try
            {
                Birim bb = db.Birim.Where(x => x.ID == b.ID).SingleOrDefault();
                if (bb != null)
                {
                    bb.Adi = b.Adi;
                    db.SaveChanges();
                    TempData["GenelMesaj"] = "Teslim birimi güncelleme işlemi başarılı bir şekilde tamamlanmıştır.";
                    return RedirectToAction("TeslimBirim");
                }
                else
                {

                    return RedirectToAction("TeslimBirim");
                }
            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }

        }

        // ---------------------------------------------------------- Ürün birimi Tanımı ---------------------------------------------------------

        public ActionResult UrunBirimTanimi()
        {
            return View(db.UrunBirim.ToList());
        }

        [HttpPost]
        public ActionResult UrunBirimEkle(UrunBirim b)
        {
            try
            {
                db.UrunBirim.Add(b);
                db.SaveChanges();
                TempData["GenelMesaj"] = "Ürün birimi ekleme işlemi başarılı bir şekilde tamamlanmıştır.";
                return RedirectToAction("UrunBirimTanimi");
            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }

        }

        [HttpPost]
        public ActionResult UrunBirimSil(int id)
        {
            UrunBirim b = db.UrunBirim.Where(x => x.ID == id).SingleOrDefault();
            if (b == null)
            {
                return Json(false);
            }
            else
            {
                try
                {
                    db.UrunBirim.Remove(b);
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
        public ActionResult UrunBirimDuzenle(int id)
        {
            UrunBirim b = db.UrunBirim.Where(x => x.ID == id).FirstOrDefault();
            return View(b);
        }

        [HttpPost]
        public ActionResult UrunBirimDuzenle(UrunBirim b)
        {
            try
            {
                UrunBirim bb = db.UrunBirim.Where(x => x.ID == b.ID).SingleOrDefault();
                if (bb != null)
                {
                    bb.Adi = b.Adi;
                    db.SaveChanges();
                    TempData["GenelMesaj"] = "Ürün birimi düzenleme işlemi başarılı bir şekilde tamamlanmıştır.";
                    return RedirectToAction("UrunBirimTanimi");
                }
                else
                {

                    return RedirectToAction("UrunBirimTanimi");
                }
            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }


        }

        // ---------------------------------------------------------- Ürün tipi Tanımı ---------------------------------------------------------

        public ActionResult UrunTipiTanimi()
        {
            return View(db.UrunTip.ToList());
        }

        [HttpPost]
        public ActionResult UrunTipiEkle(UrunTip b)
        {
            try
            {
                db.UrunTip.Add(b);
                db.SaveChanges();
                TempData["GenelMesaj"] = "Ürün Tipi ekleme işlemi başarılı bir şekilde tamamlanmıştır.";
                return RedirectToAction("UrunTipiTanimi");
            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }

        }

        [HttpPost]
        public ActionResult UrunTipiSil(int id)
        {
            UrunTip b = db.UrunTip.Where(x => x.ID == id).SingleOrDefault();
            if (b == null)
            {
                return Json(false);
            }
            else
            {
                try
                {
                    db.UrunTip.Remove(b);
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
        public ActionResult UrunTipiDuzenle(int id)
        {
            UrunTip b = db.UrunTip.Where(x => x.ID == id).FirstOrDefault();
            return View(b);
        }

        [HttpPost]
        public ActionResult UrunTipiDuzenle(UrunTip b)
        {
            try
            {
                UrunTip bb = db.UrunTip.Where(x => x.ID == b.ID).SingleOrDefault();
                if (bb != null)
                {
                    bb.Adi = b.Adi;
                    bb.Aciklama = b.Aciklama;
                    db.SaveChanges();
                    TempData["GenelMesaj"] = "Ürün Tipi düzenleme işlemi başarılı bir şekilde tamamlanmıştır.";
                    return RedirectToAction("UrunTipiTanimi");
                }
                else
                {

                    return RedirectToAction("UrunTipiTanimi");
                }
            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }

        }


    }
}