using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory_Management_Web_Application.Models;

namespace Inventory_Management_Web_Application.App_Classes
{
    public class YazilimUrunCikis
    {
        List<YazilimUrun> urunler = new List<YazilimUrun>();

        InventoryContext db = new InventoryContext();

        public void ListeyeEkle(YazilimUrun urun)
        {
            urunler.Add(urun);
        }

        public void ListedenCikart(YazilimUrun urun)
        {
            urunler.RemoveAll(x => x.ID == urun.ID);
        }

        public void ListeTemizle()
        {
            urunler.Clear();
        }

        public List<YazilimUrun> HepsiniGetir()
        {
            return urunler;
        }
    }
}