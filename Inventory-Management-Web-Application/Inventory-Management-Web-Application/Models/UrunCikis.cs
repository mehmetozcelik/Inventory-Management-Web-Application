namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UrunCikis
    {
        public int ID { get; set; }

        public int? UrunID { get; set; }

        public int? CikanMictar { get; set; }

        public int? TeslimAlanKisiID { get; set; }

        public DateTime? TeslimTarihi { get; set; }

        public int? TeslimVerenID { get; set; }

        [StringLength(500)]
        public string Aciklama { get; set; }

        public virtual Personel Personel { get; set; }

        public virtual TeslimAlanPersonel TeslimAlanPersonel { get; set; }

        public virtual Urun Urun { get; set; }

        public virtual YazılımUrun YazılımUrun { get; set; }
    }
}
