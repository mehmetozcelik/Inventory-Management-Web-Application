using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory_Management_Web_Application.Models;

namespace Inventory_Management_Web_Application.App_Classes
{
    public class UrunCikisSepet
    {
        List<UrunGiris> urunler = new List<UrunGiris>();

        InventoryContext db = new InventoryContext();

        public void ListeyeEkle(UrunGiris urun)
        {
                urunler.Add(urun);
        }

        public void ListedenCikart(UrunGiris urun)
        {
            urunler.RemoveAll(x => x.ID == urun.ID);
        }


        public void ListeTemizle()
        {
            urunler.Clear();
        }

        public List<UrunGiris> HepsiniGetir()
        {
            return urunler;
        }
    }
}