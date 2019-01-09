using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory_Management_Web_Application.App_Classes;
using Inventory_Management_Web_Application.Models;
using Inventory_Management_Web_Application.ReportFilters;

namespace Inventory_Management_Web_Application.Controllers
{
    public class YazilimUrunController : Controller
    {
        InventoryContext db = new InventoryContext();
        // GET: YazilimUrun

        [HttpGet]
        public ActionResult Listesi()
        {
            List<YazilimUrun> urunler = new List<YazilimUrun>();
            if (TempData["filtreliYazilimUrunler"] == null)
            {
                urunler = UrunList.IzinliYazilimUrunler();
            }
            else
            {
                urunler = (List<YazilimUrun>)TempData["filtreliYazilimUrunler"];
            }


            var anakategoriler = db.AnaKategori.ToList();
            ViewBag.anakategoriler = new SelectList(anakategoriler, "ID", "KategoriAdi");

            var urunbirimler = db.UrunBirim.ToList();
            ViewBag.urunbirimler = new SelectList(urunbirimler, "ID", "Adi");

            var tedarikciler = db.Tedarikci.Select(x => new
            {
                ID = x.ID,
                TedarikciAdi = x.FirmaAdi
            });
            var personeller = db.Personel.Select(x => new
            {
                ID = x.ID,
                adiSoyadi = x.Adi + " " + x.Soyadi
            });
            ViewBag.tedarikciler = new SelectList(tedarikciler, "ID", "TedarikciAdi");
            ViewBag.personeller = new SelectList(personeller, "ID", "adiSoyadi");

            ViewBag.ayarlar = db.Ayarlar.FirstOrDefault();
            return View(urunler);
        }

        [HttpPost]
        public ActionResult UrunFiltrele(YazilimUrunFilter list)
        {
            List<YazilimUrun> rapor = YazilimUrunFilter.UrunSorgu(list);
            TempData["filtreliYazilimUrunler"] = rapor;
            return RedirectToAction("Listesi");
        }

        public PartialViewResult altKategoriDropdown(int id)
        {
            var altkategoriler = db.AltKategori.Where(x => x.AnaKategorID == id).ToList();
            ViewBag.altkategoriler = new SelectList(altkategoriler, "ID", "KategoriAdi");
            return PartialView();
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            var anakategoriler = db.AnaKategori.ToList();
            ViewBag.anakategoriler = new SelectList(anakategoriler, "ID", "KategoriAdi");


            var tedarikciler = db.Tedarikci.Select(x => new
            {
                ID = x.ID,
                TedarikciAdi = x.FirmaAdi
            });
            var personeller = db.Personel.Select(x => new
            {
                ID = x.ID,
                adiSoyadi = x.Adi + " " + x.Soyadi
            });
            ViewBag.tedarikciler = new SelectList(tedarikciler, "ID", "TedarikciAdi");
            ViewBag.personeller = new SelectList(personeller, "ID", "adiSoyadi");
            ViewBag.personelleri = db.Personel.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(YazilimUrun u, string UrunSeriNo)
        {
            int Lastid = 0;
            if (db.YazilimUrun.ToList().Count != 0)
            {
                Lastid = db.YazilimUrun.Max(x => x.ID);
            }
            string urunKodu = "BISTK" + DateTime.Now.Year.ToString() + u.altKategoriID.ToString() + (Lastid + 1).ToString();
            u.UrunID = urunKodu;
            u.EklenmeTarihi = DateTime.Now;
            u.Aktif = true;
            db.YazilimUrun.Add(u);
            db.SaveChanges();

            YazilimUrun ku = db.YazilimUrun.Where(x => x.UrunID == urunKodu).SingleOrDefault();
            UrunGiris ug = new UrunGiris();
            ug.YazilimUrunID = ku.ID;
            ug.AlinanMiktar = ku.KeyAdet;
            ug.AlanPerID = u.PersonelID;
            ug.TedarikciID = u.TedarikciID;
            ug.Aciklama = u.Aciklama;
            ug.GirisTarihi = DateTime.Now;
            db.UrunGiris.Add(ug);
            db.SaveChanges();
            return RedirectToAction("Listesi");
        }

        [HttpPost]
        public ActionResult Sil(int id, string neden)
        {
            Personel aktifKullanici = (Personel)Session["Kullanici"];
            YazilimUrun b = db.YazilimUrun.Where(x => x.ID == id).SingleOrDefault();
            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;
            if (b == null)
            {
                return Json(false);
            }
            else
            {
                try
                {
                    b.Aktif = false;
                    b.SilmeNedeni = neden;
                    b.SilenKisiID = aktifKullanici.ID;
                    b.SilmeTarihi = date;
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
        public ActionResult SilinenUrunler()
        {
            return View(db.YazilimUrun.Where(x => x.Aktif == false).ToList());
        }

        [HttpPost]
        public ActionResult SilinenAktif(int id)
        {
            YazilimUrun b = db.YazilimUrun.Where(x => x.ID == id).SingleOrDefault();
            if (b == null)
            {
                return RedirectToAction("Hata", "Admin");
            }
            else
            {
                try
                {
                    b.Aktif = true;
                    ViewBag.Mesaj = "Ürün Tekrar Aktif Edilmiştir.";
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
            YazilimUrun u = db.YazilimUrun.Where(x => x.ID == id).FirstOrDefault();

            var anakategoriler = db.AltKategori.ToList();
            ViewBag.anakategoriler = new SelectList(anakategoriler, "ID", "KategoriAdi");
            if (u == null)
            {
                return RedirectToAction("Hata", "Admin");
            }
            return View(u);
        }

        [HttpPost]
        public ActionResult Guncelle(YazilimUrun u)
        {
            YazilimUrun gu = db.YazilimUrun.Where(x => x.ID == u.ID).FirstOrDefault();
            if (gu == null)
            {
                return RedirectToAction("Hata", "Admin");
            }
            gu.altKategoriID = u.altKategoriID;
            gu.UrunAdi = u.UrunAdi;
            gu.Aciklama = u.Aciklama;
            gu.KeyAdet = u.KeyAdet;
            gu.LisansBaslangicTarihi = u.LisansBaslangicTarihi;
            gu.LisansBitisTarihi = u.LisansBitisTarihi;
            //gu.UrunSeriNo = u.UrunSeriNo;
            db.SaveChanges();
            return RedirectToAction("Listesi");
        }

        // urun çıkarma
        public ActionResult stokCikar(int id)
        {
            YazilimUrun u = db.YazilimUrun.Where(x => x.ID == id).SingleOrDefault();
            if (u.KeyAdet == 0)
            {
                ViewBag.hata = "Bu ürün için stok bulunmamaktadır.";
                return RedirectToAction("Listesi");
            }
            if (u == null)
            {
                return RedirectToAction("Hata", "Admin");
            }

            var urunSepet = (App_Classes.YazilimUrunCikis)Session["YazilimUrun"];
            if (urunSepet == null)
            {
                urunSepet = new App_Classes.YazilimUrunCikis();
                Session["YazilimUrun"] = urunSepet;
            }
            urunSepet.ListeyeEkle(u);
            return RedirectToAction("Listesi");
        }

        [HttpGet]
        public ActionResult stokCikarView()
        {
            var personeller = db.TeslimAlanPersonel.Select(x => new
            {
                ID = x.ID,
                adiSoyadi = x.Adi + " " + x.Soyadi
            });
            var personellerVeren = db.Personel.Select(x => new
            {
                ID = x.ID,
                adiSoyadi = x.Adi + " " + x.Soyadi
            });
            ViewBag.Birimler = db.Birim.ToList();
            var urunbirimler = db.UrunBirim.ToList();
            ViewBag.teslimalanlar = new SelectList(personeller, "ID", "adiSoyadi");
            ViewBag.teslimverenler = new SelectList(personellerVeren, "ID", "adiSoyadi");
            ViewBag.personeller = db.Personel.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult stokCikarView(Models.UrunCikis uc)
        {
            int Lastid = 0;
            if (db.UrunCikis.Count() != 0)
            {
                Lastid = db.UrunCikis.Max(x => x.ID);
            }
            int CikisNumarasi = 1000 + DateTime.Now.Year + (Lastid + 1);
            var urunler = (App_Classes.YazilimUrunCikis)Session["YazilimUrun"];
            List<YazilimUrun> liste = urunler.HepsiniGetir();
            List<YazilimUrun> temp = new List<YazilimUrun>();
            foreach (YazilimUrun item in liste)
            {
                if (temp.Where(x => x.ID == item.ID).SingleOrDefault() != null)
                {
                    continue;
                }
                YazilimUrun stokDus = db.YazilimUrun.Where(x => x.ID == item.ID).FirstOrDefault();
                if (stokDus.KeyAdet == 0)
                {
                    ViewBag.hatali = "Çıkarılacak ürünler arasında stok miktarı 0 olan ürünler bulanmaktadır.";
                    return View();
                }
                stokDus.KeyAdet = stokDus.KeyAdet - liste.Where(x => x.ID == item.ID).ToList().Count;
                db.SaveChanges();
                uc.YazilimUrunID = item.ID;
                uc.CikisNumarasi = CikisNumarasi;
                uc.CikanMictar = liste.Where(x => x.ID == item.ID).ToList().Count;
                db.UrunCikis.Add(uc);
                db.SaveChanges();
                temp.Add(item);
            }
            urunler.ListeTemizle();
            Session.Remove("YazilimUrun");
            TempData["basariid2"] = CikisNumarasi;
            return RedirectToAction("CikisBasarili");
        }

        public ActionResult CikisBasarili()
        {
            ViewBag.id2 = TempData["basariid2"];
            return View();
        }

        [HttpGet]
        public ActionResult stokEkleView(int id)
        {
            var tedarikciler = db.Tedarikci.Select(x => new
            {
                ID = x.ID,
                TedarikciAdi = x.FirmaAdi
            });
            var personeller = db.Personel.Select(x => new
            {
                ID = x.ID,
                adiSoyadi = x.Adi + " " + x.Soyadi
            });
            var urunbirimler = db.UrunBirim.ToList();
            ViewBag.tedarikciler = new SelectList(tedarikciler, "ID", "TedarikciAdi");
            ViewBag.personeller = new SelectList(personeller, "ID", "adiSoyadi");
            ViewBag.personelleri = db.Personel.ToList();
            YazilimUrun eklenecekUrun = db.YazilimUrun.Where(x => x.ID == id).FirstOrDefault();
            return View(eklenecekUrun);
        }

        [HttpPost]
        public ActionResult stokEkle(UrunGiris veri)
        {
            var urun = db.YazilimUrun.FirstOrDefault(x => x.ID == veri.YazilimUrunID);
            urun.KeyAdet = urun.KeyAdet + veri.AlinanMiktar;
            veri.GirisTarihi = DateTime.Now;
            db.UrunGiris.Add(veri);
            db.SaveChanges();
            return RedirectToAction("urunGirisleri","YazilimUrun");
        }

        [HttpGet]
        public ActionResult SepetSil(int id)
        {
            var urunler = (App_Classes.YazilimUrunCikis)Session["YazilimUrun"];
            YazilimUrun b = db.YazilimUrun.Where(x => x.ID == id).SingleOrDefault();
            if (b == null)
            {
                RedirectToAction("Hata", "Admin");
            }
            urunler.ListedenCikart(b);
            if (urunler.HepsiniGetir().Count == 0)
            {
                urunler.ListeTemizle();
                Session.Remove("YazilimUrun");
                return RedirectToAction("Listesi");
            }
            return RedirectToAction("stokCikarView");
        }

        [HttpGet]
        public ActionResult urunGirisleri()
        {
            var urunler = db.UrunGiris.Where(x => x.YazilimUrunID != null).ToList();
            return View(urunler);
        }

        public ActionResult UrunCikislar()
        {
            return View(db.UrunCikis.Where(x => x.YazilimUrunID != null).ToList());
        }

    }
}