namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ayarlar")]
    public partial class Ayarlar
    {
        public int ID { get; set; }

        public int? YazilimUrun { get; set; }

        public int? UrunStok { get; set; }

        public int? YazilimUrunStok { get; set; }

        [StringLength(100)]
        public string mailUserName { get; set; }

        [StringLength(100)]
        public string mailPassword { get; set; }

        [StringLength(100)]
        public string mailHost { get; set; }

        public int? mailSSL { get; set; }

        public int? mailPort { get; set; }
    }
}
