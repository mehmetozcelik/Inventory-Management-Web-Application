namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KategoriRol")]
    public partial class KategoriRol
    {
        public int ID { get; set; }

        public int? RolID { get; set; }

        public int? KategoriID { get; set; }

        public virtual AltKategori AltKategori { get; set; }

        public virtual Rol Rol { get; set; }
    }
}
