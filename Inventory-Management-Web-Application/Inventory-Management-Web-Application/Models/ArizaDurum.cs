namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ArizaDurum")]
    public partial class ArizaDurum
    {
        public int ID { get; set; }

        public int? UrunID { get; set; }

        public DateTime? VerilisTarihi { get; set; }

        public DateTime? TahminiGelisTarihi { get; set; }

        [StringLength(100)]
        public string GarantiFirma { get; set; }

        [StringLength(100)]
        public string KargoNo { get; set; }

        public int? GarantiVerenKisiID { get; set; }

        public int? Adet { get; set; }

        public bool? aktif { get; set; }

        public virtual Personel Personel { get; set; }

        public virtual Urun Urun { get; set; }
    }
}
