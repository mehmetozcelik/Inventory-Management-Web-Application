using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Inventory_Management_Web_Application.Models;

namespace Inventory_Management_Web_Application.App_Classes
{
    public class UrunCikisSepet
    {
        List<UrunStok> urunler = new List<UrunStok>();

        InventoryContext db = new InventoryContext();

        public void ListeyeEkle(UrunStok urun)
        {
            UrunStok kontrol = urunler.Where(x => x.ID == urun.ID).SingleOrDefault();
            if (kontrol == null)
            {
                urunler.Add(urun);
            }
        }

        public void ListedenCikart(UrunStok urun)
        {
            urunler.RemoveAll(x => x.ID == urun.ID);
        }


        public void ListeTemizle()
        {
            urunler.Clear();
        }

        public List<UrunStok> HepsiniGetir()
        {
            return urunler;
        }
    }
}