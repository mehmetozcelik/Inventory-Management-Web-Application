using Inventory_Management_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory_Management_Web_Application.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Urun
        InventoryContext db = new InventoryContext();

        // ---------------------------------------- Ana Kategori -------------------------------------- //
        [HttpGet]
        public ActionResult AnaKategoriListesi()
        {
            return View(db.AnaKategori.ToList());
        }

        [HttpGet]
        public ActionResult AnaKategoriTanimi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AnaKategoriTanimi(AnaKategori cat)
        {
            try
            {
                db.AnaKategori.Add(cat);
                db.SaveChanges();
                TempData["GenelMesaj"] = "Kategori tanımı başarılı bir şekilde tamamlanmıştır.";
                return RedirectToAction("AnaKategoriListesi");
            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }

        }

        [HttpPost]
        public ActionResult AnaKategoriSil(int id)
        {
            AnaKategori b = db.AnaKategori.Where(x => x.ID == id).SingleOrDefault();
            if (b == null)
            {
                return Json(false);
            }
            else
            {
                try
                {
                    db.AnaKategori.Remove(b);
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
        public ActionResult AnaKategoriGuncelle(int id)
        {
            AnaKategori u = db.AnaKategori.Where(x => x.ID == id).FirstOrDefault();
            if (u == null)
            {
                return RedirectToAction("Hata", "Admin");
            }
            return View(u);
        }

        [HttpPost]
        public ActionResult AnaKategoriGuncelle(AnaKategori u)
        {
            try
            {
                AnaKategori gu = db.AnaKategori.Where(x => x.ID == u.ID).FirstOrDefault();
                if (gu == null)
                {
                    return RedirectToAction("Hata", "Admin");
                }
                gu.ID = u.ID;
                gu.KategoriAdi = u.KategoriAdi;
                gu.Aciklama = u.Aciklama;
                db.SaveChanges();
                return RedirectToAction("AnaKategoriListesi");
            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }


        }

        // ---------------------------------------- Alt Kategori -------------------------------------- //
        [HttpGet]
        public ActionResult AltKategoriListesi()
        {
            return View(db.AltKategori.ToList());
        }

        [HttpGet]
        public ActionResult AltKategoriTanimi()
        {
            var AnaKategoriler = db.AnaKategori.ToList();
            ViewBag.kategoriler = new SelectList(AnaKategoriler, "ID", "KategoriAdi");
            return View();
        }

        [HttpPost]
        public ActionResult AltKategoriTanimi(AltKategori cat)
        {
            try
            {

                db.AltKategori.Add(cat);
                db.SaveChanges();

                #region, Kategori Rolleri Ekleme
                int Lastid = 0;
                if (db.AltKategori.ToList().Count != 0)
                {
                    Lastid = db.AltKategori.Max(x => x.ID);
                }
                AltKategori k = db.AltKategori.Where(x => x.ID == Lastid).SingleOrDefault();

                int adminID = 1;
                adminID = (int)db.Rol.Where(x => x.RolAdi == "Admin").SingleOrDefault().ID;
                KategoriRol kr2 = new KategoriRol { RolID = adminID, KategoriID = k.ID };
                db.KategoriRol.Add(kr2);
                db.SaveChanges();

                Personel p = (Personel)Session["Kullanici"];
                if (p.RolID != adminID)
                {
                    KategoriRol kr = new KategoriRol { RolID = p.RolID, KategoriID = k.ID };
                    db.KategoriRol.Add(kr);
                    db.SaveChanges();
                }
                #endregion

                TempData["GenelMesaj"] = "Kategori tanımı başarılı bir şekilde tamamlanmıştır.";
                return RedirectToAction("AltKategoriListesi");
            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }

        }

        [HttpPost]
        public ActionResult AltKategoriSil(int id)
        {
            AltKategori b = db.AltKategori.Where(x => x.ID == id).SingleOrDefault();
            if (b == null)
            {
                return Json(false);
            }
            else
            {
                try
                {
                    db.AltKategori.Remove(b);
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
        public ActionResult AltKategoriGuncelle(int id)
        {

            AltKategori u = db.AltKategori.Where(x => x.ID == id).FirstOrDefault();
            if (u == null)
            {
                return RedirectToAction("Hata", "Admin");
            }
            var AnaKategoriler = db.AnaKategori.ToList();
            ViewBag.kategoriler = new SelectList(AnaKategoriler, "ID", "KategoriAdi");
            TempData["GenelMesaj"] = "Kategori güncellenmesi başarılı bir şekilde tamamlanmıştır.";
            return View(u);
        }

        [HttpPost]
        public ActionResult AltKategoriGuncelle(AltKategori u)
        {
            try
            {
                AltKategori gu = db.AltKategori.Where(x => x.ID == u.ID).FirstOrDefault();
                if (gu == null)
                {
                    return RedirectToAction("Hata", "Admin");
                }
                gu.AnaKategorID = u.AnaKategorID;
                gu.KategoriAdi = u.KategoriAdi;
                gu.Aciklama = u.Aciklama;
                db.SaveChanges();
                return RedirectToAction("AltKategoriListesi");
            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }


        }
    }
}