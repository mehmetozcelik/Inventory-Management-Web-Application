using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory_Management_Web_Application.ModelViews
{
    // Burası seçili kişilere mail atma için oluşturulmuş bir view modeldir.
    public class SelectedPersons
    {
        public int[] PersonelId { get; set; }
        public MultiSelectList Personels { get; set; }
    }
}