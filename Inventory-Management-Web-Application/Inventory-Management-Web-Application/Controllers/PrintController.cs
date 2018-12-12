using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace Inventory_Management_Web_Application.Controllers
{
    public class PrintController : Controller
    {

        public ActionResult UrunCikis(int id)
        {
            var report = new ViewAsPdf("UrunCikis")
                { };

            return report;
        }
    }
}