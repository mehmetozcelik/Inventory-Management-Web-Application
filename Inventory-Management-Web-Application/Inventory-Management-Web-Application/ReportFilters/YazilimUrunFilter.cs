using Inventory_Management_Web_Application.App_Classes;
using Inventory_Management_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Inventory_Management_Web_Application.ReportFilters
{
    public class YazilimUrunFilter
    {
        public int altKategoriID { get; set; }
        public int KeyAdet { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public DateTime LisansBaslangicTarihi { get; set; }
        public DateTime LisansBitisTarihi { get; set; }
        public int PersonelID { get; set; }
        public int TedarikciID { get; set; }

        public static List<YazilimUrun> UrunSorgu(YazilimUrunFilter list)
        {
            InventoryContext db = new InventoryContext();
            // Sorgu degiskenleri
            var props = typeof(YazilimUrunFilter).GetProperties();
            int counter = 0;
            string isim;
            StringBuilder Sorgu = new StringBuilder("SELECT * FROM YazilimUrun WHERE ");
            while (counter != props.Count())
            {
                var deger = props.ElementAt(counter).GetValue(list, null);
                isim = Convert.ToString(props.ElementAt(counter).Name.ToString());
                if (isim == "EklenmeTarihi")
                {
                    string df = "1.01.0001 00:00:00";
                    DateTime d = Convert.ToDateTime(df);
                    if ((DateTime)deger != d)
                    {
                        DateTime tarihBicim = Convert.ToDateTime(deger);
                        Sorgu.Append(isim + " >= '" + tarihBicim.Year.ToString() + "." + tarihBicim.Month.ToString() + "." + tarihBicim.Day.ToString() + "' and ");
                    }
                }
                else if (isim == "LisansBaslangicTarihi")
                {
                    string df = "1.01.0001 00:00:00";
                    DateTime d = Convert.ToDateTime(df);
                    if ((DateTime)deger != d)
                    {
                        DateTime tarihBicim = Convert.ToDateTime(deger);
                        Sorgu.Append(isim + " >= '" + tarihBicim.Year.ToString() + "." + tarihBicim.Month.ToString() + "." + tarihBicim.Day.ToString() + "' and ");
                    }
                }
                else if (isim == "LisansBitisTarihi")
                {
                    string df = "1.01.0001 00:00:00";
                    DateTime d = Convert.ToDateTime(df);
                    if ((DateTime)deger != d)
                    {
                        DateTime tarihBicim = Convert.ToDateTime(deger);
                        Sorgu.Append(isim + " >= '" + tarihBicim.Year.ToString() + "." + tarihBicim.Month.ToString() + "." + tarihBicim.Day.ToString() + "' and ");
                    }
                }
                else if ((int)deger != 0)
                {
                    if (isim == "KeyAdet")
                    {
                        Sorgu.Append(isim + " <= " + deger.ToString() + " and ");
                    }
                    else
                    {
                        Sorgu.Append(isim + " = " + deger.ToString() + " and ");
                    }
                }
                counter++;
            }
            Sorgu.Remove(Sorgu.ToString().Length - 4, 4);
            List<YazilimUrun> uruns = db.YazilimUrun.SqlQuery(Sorgu.ToString()).ToList();
            List<YazilimUrun> izinliurunler = UrunList.IzinliYazilimUrunler();
            List<YazilimUrun> donecekUrunler = new List<YazilimUrun>();
            foreach (YazilimUrun item in uruns)
            {
                bool y = izinliurunler.Exists(x => x.ID == item.ID);
                if (y)
                {
                    donecekUrunler.Add(item);
                }
            }
            return donecekUrunler;
        }
    }
}