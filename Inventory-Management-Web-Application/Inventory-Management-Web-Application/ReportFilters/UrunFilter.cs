using Inventory_Management_Web_Application.App_Classes;
using Inventory_Management_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace Inventory_Management_Web_Application.ReportFilters
{
    public class UrunFilter
    {
        public int altKategoriID { get; set; }
        public int StokMiktari { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public int PersonelID { get; set; }
        public int TedarikciID { get; set; }
        public int UrunTipID { get; set; }


        public static List<Urun> UrunSorgu(UrunFilter list)
        {
            InventoryContext db = new InventoryContext();
            // Sorgu degiskenleri
            var props = typeof(UrunFilter).GetProperties();
            int counter = 0;
            string isim;
            StringBuilder Sorgu = new StringBuilder("SELECT * FROM Urun WHERE ");
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
                else if ((int)deger != 0)
                {
                    if (isim != "StokMiktari")
                    {
                        Sorgu.Append(isim + " = " + deger.ToString() + " and ");
                    }
                    if( isim == "UrunSecenekID")
                    {
                        Sorgu.Append(isim + " = " + deger.ToString() + " and ");
                    }
                }
                counter++;
            }
            Sorgu.Remove(Sorgu.ToString().Length - 4, 4);
            List<Urun> uruns = db.Urun.SqlQuery(Sorgu.ToString()).ToList();
            List<Urun> izinliurunler = UrunList.IzinliUrunler();
            List<Urun> donecekUrunler = new List<Urun>();
            foreach (Urun item in uruns)
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