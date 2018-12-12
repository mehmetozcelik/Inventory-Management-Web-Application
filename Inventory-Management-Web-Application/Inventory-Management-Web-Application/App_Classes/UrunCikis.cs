using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory_Management_Web_Application.Models;

namespace Inventory_Management_Web_Application.App_Classes
{
    public class UrunCikis
    {
        List<Urun> urunler = new List<Urun>();

        InventoryContext db = new InventoryContext();

        public void ListeyeEkle(Urun urun)
        {
                urunler.Add(urun);
        }

        public void ListedenCikart(Urun urun)
        {
            urunler.RemoveAll(x => x.ID == urun.ID);
        }

        public void ListeTemizle()
        {
            urunler.Clear();
        }

        public List<Urun> HepsiniGetir()
        {
            return urunler;
        }
    }
}