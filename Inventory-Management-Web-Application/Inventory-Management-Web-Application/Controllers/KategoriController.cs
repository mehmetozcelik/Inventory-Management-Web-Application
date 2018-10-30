using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory_Management_Web_Application.Models;

namespace Inventory_Management_Web_Application.Controllers
{
    public class KategoriController : Controller
    {   
        InventoryContext db = new InventoryContext();

        // GET: Kategori
        public ActionResult Listesi()
        {
            return View(db.Kategori.ToList());
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View(db.Kategori.ToList());
        }

        public ActionResult Duzenle()
        {
            return View();
        }

    }
}