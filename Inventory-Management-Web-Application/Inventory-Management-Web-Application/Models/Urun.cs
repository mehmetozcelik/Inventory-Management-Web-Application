namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Urun")]
    public partial class Urun
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Urun()
        {
            ArizaDurum = new HashSet<ArizaDurum>();
            UrunCikis = new HashSet<UrunCikis>();
            UrunGiris = new HashSet<UrunGiris>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string UrunSeriNo { get; set; }

        [StringLength(100)]
        public string UrunKodu { get; set; }

        [StringLength(100)]
        public string UrunAdi { get; set; }

        public int? altKategoriID { get; set; }

        public int? StokMiktari { get; set; }

        [StringLength(500)]
        public string Aciklama { get; set; }

        public int? UrunBirimID { get; set; }

        public virtual AltKategori AltKategori { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArizaDurum> ArizaDurum { get; set; }

        public virtual UrunBirim UrunBirim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrunCikis> UrunCikis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrunGiris> UrunGiris { get; set; }
    }
}
