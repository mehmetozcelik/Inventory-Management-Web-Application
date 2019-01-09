using Inventory_Management_Web_Application.App_Classes;
using Inventory_Management_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Inventory_Management_Web_Application.ReportFilters
{
    public class CikanYazilimUrunFilter
    {


        public int YazilimUrunID { get; set; }
        public int CikanMictar { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public int TeslimVerenID { get; set; }

        public static List<UrunCikis> UrunSorgu(CikanYazilimUrunFilter list)
        {
            InventoryContext db = new InventoryContext();
            // Sorgu degiskenleri
            var props = typeof(CikanYazilimUrunFilter).GetProperties();
            int counter = 0;
            string isim;
            StringBuilder Sorgu = new StringBuilder("SELECT * FROM UrunCikis WHERE ");
            while (counter != props.Count())
            {
                var deger = props.ElementAt(counter).GetValue(list, null);
                isim = Convert.ToString(props.ElementAt(counter).Name.ToString());
                if (isim == "TeslimTarihi")
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
                    if (isim == "CikanMictar")
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

            List<UrunCikis> uruns = db.UrunCikis.SqlQuery(Sorgu.ToString()).ToList();
            List<YazilimUrun> izinliurunler = UrunList.IzinliYazilimUrunler();
            List<UrunCikis> donecekUrunler = new List<UrunCikis>();
            foreach (UrunCikis item in uruns)
            {
                bool y = izinliurunler.Exists(x => x.ID == item.YazilimUrunID);
                if (y)
                {
                    donecekUrunler.Add(item);
                }
            }
            return donecekUrunler;
        }
    }
}