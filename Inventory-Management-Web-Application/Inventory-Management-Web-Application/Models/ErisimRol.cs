namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ErisimRol")]
    public partial class ErisimRol
    {
        public int ID { get; set; }

        public int? RolID { get; set; }

        public int? ErisimID { get; set; }

        public virtual IslemErisim IslemErisim { get; set; }

        public virtual Rol Rol { get; set; }
    }
}
