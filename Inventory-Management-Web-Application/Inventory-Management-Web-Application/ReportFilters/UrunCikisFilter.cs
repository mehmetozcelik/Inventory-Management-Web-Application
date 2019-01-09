using Inventory_Management_Web_Application.App_Classes;
using Inventory_Management_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Inventory_Management_Web_Application.ReportFilters
{
    public class UrunCikisFilter
    {

        public int UrunID { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public int TeslimVerenID { get; set; }

        public static List<UrunCikis> UrunSorgu(UrunCikisFilter list)
        {
            InventoryContext db = new InventoryContext();
            // Sorgu degiskenleri
            var props = typeof(UrunCikisFilter).GetProperties();
            int counter = 0;
            string isim;
            StringBuilder Sorgu = new StringBuilder("SELECT * FROM UrunCikis WHERE ");
            int urnID=-1;
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
                        Sorgu.Append(isim + " >= '" + tarihBicim.Year.ToString() +"." + tarihBicim.Month.ToString() + "." + tarihBicim.Day.ToString() + "' and ");
                    }
                }
                else if(isim== "UrunID" && Convert.ToInt32(deger) !=0)
                {
                    urnID =Convert.ToInt32(deger);
                }
                else if ((int)deger != 0)
                {

                        Sorgu.Append(isim + " = " + deger.ToString() + " and ");
                }
                counter++;
            }
            Sorgu.Remove(Sorgu.ToString().Length - 4, 4);
           
            List<UrunCikis> uruns = db.UrunCikis.SqlQuery(Sorgu.ToString()).ToList();
            List<Urun> izinliurunler = UrunList.IzinliUrunler();
            List<UrunCikis> donecekUrunler = new List<UrunCikis>();
            foreach (UrunCikis item in uruns)
            {
                bool y = izinliurunler.Exists(x => x.ID==item.UrunStok.UrunID);
                if (y)
                {
                    if (urnID != -1)
                    {
                        if (item.UrunStok.UrunID== urnID)
                        {
                            donecekUrunler.Add(item);
                        }
                    }
                    else
                    {
                        donecekUrunler.Add(item);
                    }
                }
            }
            return donecekUrunler;
        }
    }
}