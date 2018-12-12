namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TeslimAlanPersonel")]
    public partial class TeslimAlanPersonel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TeslimAlanPersonel()
        {
            UrunCikis = new HashSet<UrunCikis>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string Adi { get; set; }

        [StringLength(100)]
        public string Soyadi { get; set; }

        public int? TeslimBirimID { get; set; }

        [StringLength(100)]
        public string Tel { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public virtual Birim Birim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrunCikis> UrunCikis { get; set; }
    }
}
