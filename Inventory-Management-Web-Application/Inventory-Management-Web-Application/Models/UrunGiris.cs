namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UrunGiris
    {
        public int ID { get; set; }

        public int? UrunID { get; set; }

        public int? AlinanMiktar { get; set; }

        public int? AlanPerID { get; set; }

        [StringLength(500)]
        public string Aciklama { get; set; }

        public int? TedarikciID { get; set; }

        public int? YazilimUrunID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GirisTarihi { get; set; }

        [StringLength(100)]
        public string UrunSeriNo { get; set; }

        public virtual Personel Personel { get; set; }

        public virtual Tedarikci Tedarikci { get; set; }

        public virtual Urun Urun { get; set; }

        public virtual YazılımUrun YazılımUrun { get; set; }
    }
}
