using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory_Management_Web_Application.Models;

namespace Inventory_Management_Web_Application.App_Classes
{
    public class YazilimUrunCikis
    {
        List<YazılımUrun> urunler = new List<YazılımUrun>();

        InventoryContext db = new InventoryContext();

        public void ListeyeEkle(YazılımUrun urun)
        {
            urunler.Add(urun);
        }

        public void ListedenCikart(YazılımUrun urun)
        {
            urunler.RemoveAll(x => x.ID == urun.ID);
        }

        public void ListeTemizle()
        {
            urunler.Clear();
        }

        public List<YazılımUrun> HepsiniGetir()
        {
            return urunler;
        }
    }
}