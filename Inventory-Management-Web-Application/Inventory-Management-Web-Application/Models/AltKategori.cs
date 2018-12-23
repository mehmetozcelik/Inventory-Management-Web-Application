namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AltKategori")]
    public partial class AltKategori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AltKategori()
        {
            KategoriRol = new HashSet<KategoriRol>();
            Urun = new HashSet<Urun>();
            YazılımUrun = new HashSet<YazılımUrun>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string KategoriAdi { get; set; }

        [StringLength(500)]
        public string Aciklama { get; set; }

        public int? AnaKategorID { get; set; }

        public virtual AnaKategori AnaKategori { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KategoriRol> KategoriRol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urun> Urun { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YazılımUrun> YazılımUrun { get; set; }
    }
}
