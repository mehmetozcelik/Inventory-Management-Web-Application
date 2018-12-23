namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IslemErisim")]
    public partial class IslemErisim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IslemErisim()
        {
            ErisimRol = new HashSet<ErisimRol>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string Adı { get; set; }

        [StringLength(100)]
        public string Controller { get; set; }

        [StringLength(100)]
        public string Action { get; set; }

        [StringLength(100)]
        public string MenuList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ErisimRol> ErisimRol { get; set; }
    }
}
