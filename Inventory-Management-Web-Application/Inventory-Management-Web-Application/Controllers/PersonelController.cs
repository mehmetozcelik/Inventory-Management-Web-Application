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
            //ViewBag.Birimler = db.Birim.ToList();
            ViewBag.Birimler = new SelectList(db.Birim.ToList(), "ID", "Adi");
            return View();
        }

        [HttpPost]
        public ActionResult TaEkle(TeslimAlanPersonel veri, int? control)
        {

            try
            {
                if (db.TeslimAlanPersonel.FirstOrDefault(x => x.Email == veri.Email) == null)
                {
                    db.TeslimAlanPersonel.Add(veri);
                    db.SaveChanges();
                    if (control == 1)
                    {
                        return RedirectToAction("stokCikarView", "Urun");
                    }
                    else if (control == 0)
                    {
                        return RedirectToAction("stokCikarView", "YazilimUrun");
                    }
                    TempData["GenelMesaj"] = " işlemi başarılı bir şekilde tamamlanmıştır.";
                }
                else
                {
                    TempData["Hata"] = "Girmiş Olduğunuz E-posta Adresi Kullanılmaktadır.";
                }
                
                return RedirectToAction("TaListesi");
            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }

        }

        public ActionResult TaDuzenle(int ID)
        {
            TeslimAlanPersonel teslimAlanPersonel = db.TeslimAlanPersonel.SingleOrDefault(x => x.ID == ID);
            ViewBag.Birimler = new SelectList(db.Birim.ToList(), "ID", "Adi");
            return View(teslimAlanPersonel);
        }

        [HttpPost]
        public ActionResult TaDuzenle(TeslimAlanPersonel veri)
        {
            try
            {
                if (db.TeslimAlanPersonel.FirstOrDefault(x => x.Email == veri.Email && x.ID != veri.ID) == null)
                {
                    db.Entry(veri).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["GenelMesaj"] = " işlemi başarılı bir şekilde tamamlanmıştır.";
                }
                else
                {
                    TempData["Hata"] = "Girmiş Olduğunuz E-posta Adresi Kullanılmaktadır.";
                }              
                return RedirectToAction("TaListesi");

            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }

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
                try
                {
                    db.TeslimAlanPersonel.Remove(b);
                    db.SaveChanges();
                    return Json(true);
                }
                catch (Exception)
                {
                    return Json("FK");
                }

            }
        }
    }
}