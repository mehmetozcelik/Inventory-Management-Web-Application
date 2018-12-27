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
        public ActionResult Ayarlar()
        {
            return View(db.Ayarlar.FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Ayarlar(Ayarlar ayarlar)
        {
            Ayarlar a = db.Ayarlar.FirstOrDefault();
            a.UrunStok = ayarlar.UrunStok;
            a.YazilimUrun = ayarlar.YazilimUrun;
            a.YazilimUrunStok = ayarlar.YazilimUrunStok;
            db.SaveChanges();
            return View(db.Ayarlar.FirstOrDefault());
        }

        public ActionResult CikisYap()
        {
            Session.Abandon();
            return RedirectToAction("Login","Kullanici");
        }

        public PartialViewResult MenuGetir()
        {
            Personel p = (Personel)Session["Kullanici"];

            MenuControl k = new MenuControl();
            k.menuler = db.Menu.ToList();
            k.roller = db.MenuRol.Where(x=>x.RolID==p.RolID).ToList();
            return  PartialView(k);
        }

        [HttpGet]
        public ActionResult YetkiBulunamadi()
        {
            return View();
        }
    }
}