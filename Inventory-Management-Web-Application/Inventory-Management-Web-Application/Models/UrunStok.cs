namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UrunStok")]
    public partial class UrunStok
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UrunStok()
        {
            ArizaDurum = new HashSet<ArizaDurum>();
            UrunCikis = new HashSet<UrunCikis>();
            UrunGiris = new HashSet<UrunGiris>();
        }

        public int ID { get; set; }

        public int? UrunID { get; set; }

        [StringLength(100)]
        public string UrunSeriNo { get; set; }

        public bool? Aktif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArizaDurum> ArizaDurum { get; set; }

        public virtual Urun Urun { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrunCikis> UrunCikis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrunGiris> UrunGiris { get; set; }
    }
}
