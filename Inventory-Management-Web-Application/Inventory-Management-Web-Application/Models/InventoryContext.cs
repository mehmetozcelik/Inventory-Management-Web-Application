namespace Inventory_Management_Web_Application.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class InventoryContext : DbContext
    {
        public InventoryContext()
            : base("name=InventoryContext")
        {
        }

        public virtual DbSet<AltKategori> AltKategori { get; set; }
        public virtual DbSet<AnaKategori> AnaKategori { get; set; }
        public virtual DbSet<ArizaDurum> ArizaDurum { get; set; }
        public virtual DbSet<Ayarlar> Ayarlar { get; set; }
        public virtual DbSet<Birim> Birim { get; set; }
        public virtual DbSet<ErisimRol> ErisimRol { get; set; }
        public virtual DbSet<IslemErisim> IslemErisim { get; set; }
        public virtual DbSet<KategoriRol> KategoriRol { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuRol> MenuRol { get; set; }
        public virtual DbSet<Personel> Personel { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tedarikci> Tedarikci { get; set; }
        public virtual DbSet<TeslimAlanPersonel> TeslimAlanPersonel { get; set; }
        public virtual DbSet<Urun> Urun { get; set; }
        public virtual DbSet<UrunBirim> UrunBirim { get; set; }
        public virtual DbSet<UrunCikis> UrunCikis { get; set; }
        public virtual DbSet<UrunGiris> UrunGiris { get; set; }
        public virtual DbSet<YazılımUrun> YazılımUrun { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AltKategori>()
                .HasMany(e => e.KategoriRol)
                .WithOptional(e => e.AltKategori)
                .HasForeignKey(e => e.KategoriID);

            modelBuilder.Entity<AnaKategori>()
                .HasMany(e => e.AltKategori)
                .WithOptional(e => e.AnaKategori)
                .HasForeignKey(e => e.AnaKategorID);

            modelBuilder.Entity<Birim>()
                .HasMany(e => e.TeslimAlanPersonel)
                .WithOptional(e => e.Birim)
                .HasForeignKey(e => e.TeslimBirimID);

            modelBuilder.Entity<IslemErisim>()
                .HasMany(e => e.ErisimRol)
                .WithOptional(e => e.IslemErisim)
                .HasForeignKey(e => e.ErisimID);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.Menu1)
                .WithOptional(e => e.Menu2)
                .HasForeignKey(e => e.ParentMenuID);

            modelBuilder.Entity<Personel>()
                .HasMany(e => e.ArizaDurum)
                .WithOptional(e => e.Personel)
                .HasForeignKey(e => e.GarantiVerenKisiID);

            modelBuilder.Entity<Personel>()
                .HasMany(e => e.Urun)
                .WithOptional(e => e.Personel)
                .HasForeignKey(e => e.SilenKisiID);

            modelBuilder.Entity<Personel>()
                .HasMany(e => e.UrunCikis)
                .WithOptional(e => e.Personel)
                .HasForeignKey(e => e.TeslimVerenID);

            modelBuilder.Entity<Personel>()
                .HasMany(e => e.UrunGiris)
                .WithOptional(e => e.Personel)
                .HasForeignKey(e => e.AlanPerID);

            modelBuilder.Entity<Personel>()
                .HasMany(e => e.YazılımUrun)
                .WithOptional(e => e.Personel)
                .HasForeignKey(e => e.SilenKisiID);

            modelBuilder.Entity<TeslimAlanPersonel>()
                .HasMany(e => e.UrunCikis)
                .WithOptional(e => e.TeslimAlanPersonel)
                .HasForeignKey(e => e.TeslimAlanKisiID);

            modelBuilder.Entity<YazılımUrun>()
                .HasMany(e => e.UrunCikis)
                .WithOptional(e => e.YazılımUrun)
                .HasForeignKey(e => e.YazilimUrunID);

            modelBuilder.Entity<YazılımUrun>()
                .HasMany(e => e.UrunGiris)
                .WithOptional(e => e.YazılımUrun)
                .HasForeignKey(e => e.YazilimUrunID);
        }
    }
}
