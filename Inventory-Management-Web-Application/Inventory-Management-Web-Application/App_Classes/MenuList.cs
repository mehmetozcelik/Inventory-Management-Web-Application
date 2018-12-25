using Inventory_Management_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Inventory_Management_Web_Application.App_Classes
{
   
    public class MenuList
    {

        public bool KullaniciIslemleri { get; set; }
        public bool ProfilTanimi { get; set; }
        public bool KullaniciListesi { get; set; }
        public bool KullaniciEkle { get; set; }
        public bool taPersonelListesi { get; set; }
        public bool taPersonelEkle { get; set; }
        public bool KategoriIslemleri { get; set; }
        public bool AnaKategoriListesi { get; set; }
        public bool AnaKategoriTanimi { get; set; }
        public bool AltKategoriListesi { get; set; }
        public bool AltKategoriTanimi { get; set; }
        public bool UrunIslemleri { get; set; }
        public bool UrunListesi { get; set; }
        public bool UrunEkle { get; set; }
        public bool urunGirisleri { get; set; }
        public bool UrunCikislar { get; set; }
        public bool YazilimUrunIslemleri { get; set; }
        public bool YazilimUrunListesi { get; set; }
        public bool YazilimUrunEkle { get; set; }
        public bool YazilimUrunGirisler { get; set; }
        public bool YazilimUrunCikislar { get; set; }
        public bool UrunAriziIslemleri { get; set; }
        public bool GarantiListesi { get; set; }
        public bool EskiGarantiListesi { get; set; }
        public bool TedarikciIslemleri { get; set; }
        public bool TedarikciListesi { get; set; }
        public bool TedarikciEkle { get; set; }
        public bool Raporlar { get; set; }
        public bool RaporUrun { get; set; }
        public bool RaporCikanUrun { get; set; }
        public bool RaporYazilimUrun { get; set; }
        public bool RaporCikanYazilimUrun { get; set; }
        public bool Tanimlar { get; set; }
        public bool TeslimBirim { get; set; }
        public bool UrunBirimTanimi { get; set; }
        public bool Ayarlar { get; set; }
        public bool SilinenUrunler { get; set; }
        public bool SilinenYazilimUrunler { get; set; }

        public static void RolKontrol(MenuList list ,int RolID)
        {
            InventoryContext db = new InventoryContext();
            
            //Rol değer listesi
            List<int> termsList = new List<int>();

            // Rol liste ve değerleri alma
            var props = typeof(MenuList).GetProperties();
            int counter = 0;
            bool deger;
            string isim;
            while (counter != props.Count())
            {
                deger = Convert.ToBoolean(props.ElementAt(counter).GetValue(list, null));
                isim = Convert.ToString(props.ElementAt(counter).Name.ToString());
                if (deger)
                {
                    int id = db.Menu.Where(x => x.MenuList == isim).FirstOrDefault().ID;
                    termsList.Add(id);
                }
                counter++;
            }

            MenuRol menu = new MenuRol();

            foreach (var item in termsList)
            {
                menu.MenuID = item;
                menu.RolID = RolID;
                db.MenuRol.Add(menu);
                db.SaveChanges();
            }
        }
    }
}