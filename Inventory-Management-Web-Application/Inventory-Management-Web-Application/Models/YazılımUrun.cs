namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class YazılımUrun
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public YazılımUrun()
        {
            UrunCikis = new HashSet<UrunCikis>();
            UrunGiris = new HashSet<UrunGiris>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string UrunID { get; set; }

        [StringLength(100)]
        public string UrunAdi { get; set; }

        [StringLength(500)]
        public string Aciklama { get; set; }

        public DateTime? LisansBaslangicTarihi { get; set; }

        public DateTime? LisansBitisTarihi { get; set; }

        public int? KeyAdet { get; set; }

        [StringLength(100)]
        public string UrunSeriNo { get; set; }

        [StringLength(100)]
        public string LisansUyari { get; set; }

        public int? altKategoriID { get; set; }

        public int? TeslimAlanKisiID { get; set; }

        public virtual AltKategori AltKategori { get; set; }

        public virtual TeslimAlanPersonel TeslimAlanPersonel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrunCikis> UrunCikis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrunGiris> UrunGiris { get; set; }
    }
}
