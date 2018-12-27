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
            #region,LDAP ile giriş kontrolu
            bool ldap = Functions.LDAP(mail, sifre);
            if (ldap)
            {
                Personel ldapPersonel = new Personel();
                ldapPersonel.ID = -1;
                ldapPersonel.Adi = "LDAP";
                ldapPersonel.Soyadi = "Personel";
                ldapPersonel.Email = mail;
                ldapPersonel.RolID = db.Rol.Where(x=>x.RolAdi=="LDAP").FirstOrDefault().ID;
                Session["Kullanici"] = ldapPersonel;
                return RedirectToAction("Index", "Admin");
            }
            #endregion

            #region,Personel ile giriş kontrolu
            Personel p = db.Personel.Where(x => x.Email == mail && x.Sifre == sifre).FirstOrDefault();

            if (p==null)
            {
                ViewBag.Hata = "Girdiğiniz Bilgilerde Bir kullanıcı Bulunamadı";
                return View();
            }

            Session["Kullanici"] = p;
            return RedirectToAction("Index", "Admin");
            #endregion
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

            ViewBag.KategoriRolleri = db.KategoriRol.Where(x => x.RolID == r.ID).ToList();
            ViewBag.Kategoriler = db.AltKategori.ToList();

            ViewBag.ErisimYetkileri = db.ErisimRol.Where(x => x.RolID == r.ID).ToList();
            ViewBag.Erisim = db.IslemErisim.ToList();

            ViewBag.Yetkileri = db.MenuRol.Where(x => x.RolID == r.ID).ToList();
            ViewBag.Menuler = db.Menu.ToList();
            return View(r);
        }

        [HttpPost]
        public ActionResult Yetkiler(int RolID, string menuler , string islemler , string katrol)
        {
            //, MenuList list , IslemErisimList list2
            Rol r = db.Rol.Where(x => x.ID == RolID).FirstOrDefault(); // Düzenlenmek istenen Rolu bul

            if (r == null) // rol boş ise hata döndür
            {
                return RedirectToAction("Hata", "Admin");
            }

            #region,Menü Rolleri update
            //Bu role ait tüm yetkileri 
            List<MenuRol> menuRol = db.MenuRol.Where(x => x.RolID == r.ID).ToList();

          
                // Menü rollerinin silinmesi
                foreach (var item in menuRol)
                {
                    db.MenuRol.Remove(item);
                }
                db.SaveChanges(); // roller sıfırlandı.
         
            //Tüm rolleri yeniden yükle ve değişiklikleri kayıt et.
            string[] Menuparts = menuler.Split('^');
            List<Menu> Eklenenmenuler = new List<Menu>();
            for (int i = 0; i < Menuparts.Length; i++)
            {
                string s = Menuparts[i].ToString();
                Menu alt = db.Menu.Where(x => x.Adi == s).FirstOrDefault();
                if (alt != null)
                {
                    Eklenenmenuler.Add(alt);
                }
            }
            foreach (Menu item in Eklenenmenuler)
            {
                MenuRol rol = new MenuRol();
                rol.MenuID = item.ID;
                rol.RolID = RolID;
                db.MenuRol.Add(rol);
                db.SaveChanges();
            }
            // MenuList.RolKontrol(list, RolID);
            ViewBag.Yetkileri = db.MenuRol.Where(x => x.RolID == r.ID).ToList();
            ViewBag.Menuler = db.Menu.ToList();
            #endregion

            #region,İşlem Rolleri Update
            //Bu role ait post izinleri
            List<ErisimRol> erisimRol = db.ErisimRol.Where(x => x.RolID == r.ID).ToList();

           
                // Erisim rollerinin silinmesi
                foreach (var item in erisimRol)
                {
                    db.ErisimRol.Remove(item);
                }
                db.SaveChanges(); // roller sıfırlandı.
  
    

            string[] Islemparts = islemler.Split('^');
            List<IslemErisim> islemlerim = new List<IslemErisim>();
            for (int i = 0; i < Islemparts.Length; i++)
            {
                string s = Islemparts[i].ToString();
                IslemErisim islemi = db.IslemErisim.Where(x => x.Adı == s).FirstOrDefault();
                if (islemi != null)
                {
                    islemlerim.Add(islemi);
                }
            }

            //Tüm erisimleri yeniden yükle ve değişiklikleri kayıt et.
            foreach (IslemErisim item in islemlerim)
            {
                ErisimRol rol = new ErisimRol();
                rol.ErisimID = item.ID;
                rol.RolID = RolID;
                db.ErisimRol.Add(rol);
                db.SaveChanges();

            }
            #endregion

            #region, ürün Kategori rolleri
            string[] parts = katrol.Split('^');
            List<AltKategori> alts = new List<AltKategori>(); 
            for (int i = 0; i < parts.Length; i++)
            {
                string s = parts[i].ToString();
                AltKategori alt = db.AltKategori.Where(x => x.KategoriAdi == s).FirstOrDefault();
                if (alt !=null)
                {
                    alts.Add(alt);
                }
            }
            List<KategoriRol> kr = db.KategoriRol.Where(x => x.RolID == RolID).ToList();
        
                foreach (var item in kr) // tüm kategori rollerini sil.
                {
                    db.KategoriRol.Remove(item);
                }
                db.SaveChanges();
           
      
            //tüm rolleri yeniden yükle
            foreach (AltKategori item in alts)
            {
                KategoriRol ktr = new KategoriRol();
                ktr.RolID = RolID;
                ktr.KategoriID = item.ID;
                db.KategoriRol.Add(ktr);
                db.SaveChanges();
            }
            #endregion
            //Sayfayı geri yükle
            TempData["Uyari"] = "Profil yetkileri başarılı bir şekilde güncellenmiştir.";
            return RedirectToAction("ProfilListesi");
        }

        [HttpPost]
        public ActionResult YetkiSil(int id)
        {
            Rol b = db.Rol.Where(x => x.ID == id).SingleOrDefault();
            if (b == null)
            {
                return Json(false);
            }
            else
            {
                try
                {
                    if (b.RolAdi=="Admin")
                    {
                        return Json("admin");
                    }
                    if (b.RolAdi== "LDAP")
                    {
                        return Json("ldap");
                    }
                    db.Rol.Remove(b);
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
        public ActionResult YetkiDuzenle(int id)
        {
            Rol r = db.Rol.Where(x => x.ID == id).FirstOrDefault();
            return View(r);
        }

        [HttpPost]
        public ActionResult YetkiDuzenle(Rol rol)
        {
            Rol r = db.Rol.Where(x => x.ID == rol.ID).FirstOrDefault();
            if (r==null)
            {
                return RedirectToAction("Hata", "Admin");
            }
            r.RolAdi = rol.RolAdi;
            r.Aciklama = rol.Aciklama;
            db.SaveChanges();
            TempData["Uyari"] = "Profil bilgileri başarılı bir şekilde güncellenmiştir.";
            return RedirectToAction("ProfilListesi");
        }

    }
}