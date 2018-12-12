using Inventory_Management_Web_Application.App_Classes;
using Inventory_Management_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Inventory_Management_Web_Application.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        InventoryContext db = new InventoryContext();

        //----------------------- Kullanıcı İşlemleri ------------------------------------
        [HttpGet]
        public ActionResult Listesi()
        {
            return View(db.Personel.ToList());
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            var Roller = db.Rol.ToList();
            ViewBag.Roller = new SelectList(Roller, "ID", "RolAdi");
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Personel p)
        {
            db.Personel.Add(p);
            db.SaveChanges();
            return RedirectToAction("Listesi");
        }

        [HttpPost]
        public ActionResult Sil(int id)
        {
            Personel b = db.Personel.Where(x => x.ID == id).SingleOrDefault();
            if (b == null)
            {
                return Json(false);
            }
            else
            {
                try
                {
                    db.Personel.Remove(b);
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
            Personel u = db.Personel.Where(x => x.ID == id).FirstOrDefault();
            var Roller = db.Rol.ToList();
            ViewBag.Roller = new SelectList(Roller, "ID", "RolAdi");
            if (u == null)
            {
                return RedirectToAction("Hata", "Admin");
            }
            return View(u);
        }

        [HttpPost]
        public ActionResult Guncelle(Personel u)
        {
            Personel gu = db.Personel.Where(x => x.ID == u.ID).FirstOrDefault();
            if (gu == null)
            {
                return RedirectToAction("Hata", "Admin");
            }
            gu.Adi = u.Adi;
            gu.Soyadi = u.Soyadi;
            gu.Email = u.Email;
            gu.Sifre = u.Sifre;
            gu.Tel = u.Tel;
            gu.RolID = u.RolID;
            db.SaveChanges();
            return RedirectToAction("Listesi");
        }


        //----------------------- Diğer İşlemler -----------------------------------------
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string mail , string sifre)
        {
            bool ldap = Functions.LDAP(mail, sifre);

            if (ldap)
            {
                return RedirectToAction("Index", "Admin");
            }

            Personel p = db.Personel.Where(x => x.Email == mail && x.Sifre == sifre).FirstOrDefault();

            if (p==null)
            {
                ViewBag.Hata = "Girdiğiniz Bilgilerde Bir kullanıcı Bulunamadı";
                return View();
            }

            Session["Kullanici"] = p;

            return RedirectToAction("Index","Admin");
        }

        [HttpGet]
        public ActionResult ProfilListesi()
        {
            return View(db.Rol.ToList());
        }

        [HttpPost]
        public ActionResult ProfilEkle(Rol r)
        {
            db.Rol.Add(r);
            db.SaveChanges();
            return RedirectToAction("ProfilListesi");
        }

        [HttpGet]
        public ActionResult Yetkiler(int id)
        {
            Rol r = db.Rol.Where(x => x.ID == id).FirstOrDefault();

            if (r==null)
            {
                return RedirectToAction("Hata", "Admin");
            }

            ViewBag.Yetkileri = db.MenuRol.Where(x => x.RolID == r.ID).ToList();
            ViewBag.Menuler = db.Menu.ToList();
            return View(r);
        }

        [HttpPost]
        public ActionResult Yetkiler(int RolID,MenuList list)
        {

            Rol r = db.Rol.Where(x => x.ID == RolID).FirstOrDefault(); // Düzenlenmek istenen Rolu bul

            if (r == null) // rol boş ise hata döndür
            {
                return RedirectToAction("Hata", "Admin");
            }

            //Bu role ait tüm yetkileri sil
            List<MenuRol> menuRol = db.MenuRol.Where(x => x.RolID == r.ID).ToList();

            foreach (var item in menuRol)
            {
                db.MenuRol.Remove(item);
            }

            db.SaveChanges(); // roller sıfırlandı.

            //Tüm rolleri yeniden yükle ve değişiklikleri kayıt et.
            MenuList.RolKontrol(list, RolID);

            //Sayfayı geri yükle
            ViewBag.Yetkileri = db.MenuRol.Where(x => x.RolID == r.ID).ToList();
            ViewBag.Menuler = db.Menu.ToList();
            return View(r);
        }


    }
}