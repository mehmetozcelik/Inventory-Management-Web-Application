using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using Inventory_Management_Web_Application.Models;

namespace Inventory_Management_Web_Application.Controllers
{
    public class PrintController : Controller
    {
        InventoryContext db = new InventoryContext();

        public ActionResult UrunCikis(int id)
        {
            try
            {
                List<UrunCikis> uc = db.UrunCikis.Where(x => x.CikisNumarasi == id).ToList();
                var report = new ViewAsPdf("UrunCikis", uc)
                { };
                return report;

            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }

        }

        public ActionResult yazilimUrunCikis(int id)
        {
            try
            {
                List<UrunCikis> uc = db.UrunCikis.Where(x => x.CikisNumarasi == id).ToList();
                var report = new ViewAsPdf("yazilimUrunCikis", uc)
                { };
                return report;
            }
            catch (Exception)
            {
                return Redirect("/Admin/Hata");
            }

        }
    }
}