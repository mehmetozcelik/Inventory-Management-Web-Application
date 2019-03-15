using Inventory_Management_Web_Application.App_Classes;
using Inventory_Management_Web_Application.Models;
using Inventory_Management_Web_Application.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory_Management_Web_Application.Controllers
{
    public class AdminController : Controller
    {

        InventoryContext db = new InventoryContext();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.yazilimlar = UrunList.IzinliYazilimUrunler();
            ViewBag.urunler = UrunList.IzinliUrunler();

            ViewBag.yazilimsayisi = UrunList.IzinliYazilimUrunler().Count;
            ViewBag.urunsayisi = UrunList.IzinliUrunler().Count;
            ViewBag.kullanicisayisi = db.Personel.ToList().Count;
            ViewBag.tedarikcisayisi = db.Tedarikci.ToList().Count;
            ViewBag.ayarlar = db.Ayarlar.FirstOrDefault();
            return View();
        }

        public ActionResult Hata()
        {
            return View();
        }

        [HttpGet]
        public ActionResult sistemHata(string hata)
        {
            ViewBag.gelenhata = hata;
            return View();
        }

        [HttpGet]
        public ActionResult Ayarlar()
        {

            var personelleri = db.Personel.Select(x => new
            {
                ID = x.ID,
                adiSoyadi = x.Adi + " " + x.Soyadi
            });

            Ayarlar ayar = db.Ayarlar.FirstOrDefault();
            string selectedPersones = ayar.UserBilgiMail;
            int[] pers = selectedPersones.Split('-').Select(n => Convert.ToInt32(n)).ToArray();
            Array.Reverse(pers);

           ViewBag.kisiler = new MultiSelectList(personelleri, "ID", "adiSoyadi",new int[] {2,3});
            return View(db.Ayarlar.FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Ayarlar(Ayarlar ayarlar , int [] kisiler)
        {
            try
            {
                Ayarlar a = db.Ayarlar.FirstOrDefault();
                a.UrunStok = ayarlar.UrunStok;
                a.YazilimUrun = ayarlar.YazilimUrun;
                a.YazilimUrunStok = ayarlar.YazilimUrunStok;
                string ids = "";
                //altsergiler
                for (int i = 0; i < kisiler.Length; i++)
                {
                    if (i == kisiler.Length)
                    {
                        continue;
                    }
                    ids = kisiler[i].ToString() + "-" + ids;
                }
                ids = ids.Remove(ids.Length - 1, 1);
                a.UserBilgiMail = ids;
                db.SaveChanges();
                TempData["GenelMesaj"] = "Ayarlar başarılı bir şekilde güncellenmiştir.";
                var personelleri = db.Personel.Select(x => new
                {
                    ID = x.ID,
                    adiSoyadi = x.Adi + " " + x.Soyadi
                });

                Ayarlar ayar = db.Ayarlar.FirstOrDefault();
                string selectedPersones = ayar.UserBilgiMail;
                int[] pers = selectedPersones.Split('-').Select(n => Convert.ToInt32(n)).ToArray();

                


                ViewBag.kisiler = new MultiSelectList(personelleri, "ID", "adiSoyadi", kisiler);
                return View(db.Ayarlar.FirstOrDefault());
            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }

        }

        public ActionResult CikisYap()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Kullanici");
        }

        public PartialViewResult MenuGetir()
        {
            Personel p = (Personel)Session["Kullanici"];

            MenuControl k = new MenuControl();
            k.menuler = db.Menu.ToList();
            k.roller = db.MenuRol.Where(x => x.RolID == p.RolID).ToList();
            return PartialView(k);
        }

        [HttpGet]
        public ActionResult YetkiBulunamadi()
        {
            return View();
        }
    }
}