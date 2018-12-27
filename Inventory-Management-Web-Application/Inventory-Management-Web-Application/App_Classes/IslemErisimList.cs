using Inventory_Management_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory_Management_Web_Application.App_Classes
{
    public class IslemErisimList
    {
        public bool IAyarlar { get; set; }
        public bool IKullaniciEkle { get; set; }
        public bool IKullaniciSil { get; set; }
        public bool IKullaniciGuncelle { get; set; }
        public bool IKullaniciYetkiler { get; set; }

        public static void RolKontrol(IslemErisimList list, int RolID)
        {
            InventoryContext db = new InventoryContext();

            //Rol değer listesi
            List<int> termsList = new List<int>();

            // Rol liste ve değerleri alma
            var props = typeof(IslemErisimList).GetProperties();
            int counter = 0;
            bool deger;
            string isim;
            while (counter != props.Count())
            {
                deger = Convert.ToBoolean(props.ElementAt(counter).GetValue(list, null));
                isim = Convert.ToString(props.ElementAt(counter).Name.ToString());
                if (deger)
                {
                    int id = db.IslemErisim.Where(x => x.MenuList == isim).FirstOrDefault().ID;
                    termsList.Add(id);
                }
                counter++;
            }

            ErisimRol erisim = new ErisimRol();

            foreach (var item in termsList)
            {
                erisim.ErisimID = item;
                erisim.RolID = RolID;
                db.ErisimRol.Add(erisim);
                db.SaveChanges();
            }
        }
    }
}