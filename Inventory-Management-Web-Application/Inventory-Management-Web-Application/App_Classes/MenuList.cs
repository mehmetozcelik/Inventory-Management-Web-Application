using Inventory_Management_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory_Management_Web_Application.App_Classes
{
   
    public class MenuList
    {     
        public bool UrunIslemleri { get; set; }
        public bool UrunListesi { get; set; }
        public bool YazilimListesi { get; set; }
        public bool KullaniciIslemleri { get; set; }
        public bool ProfilTanimi { get; set; }
        public bool KullaniciEkle { get; set; }

        public static void RolKontrol(MenuList list ,int RolID)
        {
            InventoryContext db = new InventoryContext();

            List<int> termsList = new List<int>();

            if (list.KullaniciEkle)
            {
                int id = db.Menu.Where(x => x.MenuList == "KullaniciEkle").FirstOrDefault().ID;
                termsList.Add(id);
            }
            if (list.UrunListesi)
            {
                int id = db.Menu.Where(x => x.MenuList == "UrunListesi").FirstOrDefault().ID;
                termsList.Add(id);
            }
            if (list.YazilimListesi)
            {
                int id = db.Menu.Where(x => x.MenuList == "YazilimListesi").FirstOrDefault().ID;
                termsList.Add(id);
            }
            if (list.KullaniciIslemleri)
            {
                int id = db.Menu.Where(x => x.MenuList == "KullaniciIslemleri").FirstOrDefault().ID;
                termsList.Add(id);
            }
            if (list.ProfilTanimi)
            {
                int id = db.Menu.Where(x => x.MenuList == "ProfilTanimi").FirstOrDefault().ID;
                termsList.Add(id);
            }
            if (list.KullaniciEkle)
            {
                int id = db.Menu.Where(x => x.MenuList == "KullaniciEkle").FirstOrDefault().ID;
                termsList.Add(id);
            }

            MenuRol menu = new MenuRol();

            foreach (var item in termsList)
            {
                menu.MenuID = item;
                menu.RolID = RolID;
                db.MenuRol.Add(menu);
            }
            db.SaveChanges();
        }
    }
}