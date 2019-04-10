namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UrunSecenek")]
    public partial class UrunSecenek
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Adi { get; set; }

        [StringLength(100)]
        public string Aciklama { get; set; }

        public int? UrunTipID { get; set; }

        public virtual UrunTip UrunTip { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Urun> Urun { get; set; }
    }
}
