using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Inventory_Management_Web_Application.App_Classes
{
    public class KategoriList
    {
        public string PreprationRequired { get; set; }
        public List<CheckBoxes> lstPreprationRequired { get; set; }
        public string[] CategoryIds { get; set; }
    }
}