namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ArizaEskiKayitlar")]
    public partial class ArizaEskiKayitlar
    {
        public int ID { get; set; }

        public int? ArizaDurumID { get; set; }

        public int? GarantiAlanID { get; set; }

        public DateTime? StokEklenmeTarihi { get; set; }

        public virtual ArizaDurum ArizaDurum { get; set; }

        public virtual Personel Personel { get; set; }
    }
}
