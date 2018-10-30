namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tedarikci")]
    public partial class Tedarikci
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tedarikci()
        {
            UrunGiris = new HashSet<UrunGiris>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string FirmaAdi { get; set; }

        [StringLength(20)]
        public string FirmaTel { get; set; }

        [StringLength(500)]
        public string FirmaAdres { get; set; }

        [StringLength(100)]
        public string FirmaMail { get; set; }

        [StringLength(100)]
        public string YetkiliAdi { get; set; }

        [StringLength(100)]
        public string YetkiliSoyadi { get; set; }

        [StringLength(100)]
        public string YetkiliUnvani { get; set; }

        [StringLength(20)]
        public string YetkiliTel { get; set; }

        [StringLength(100)]
        public string YetkiliMail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrunGiris> UrunGiris { get; set; }
    }
}
