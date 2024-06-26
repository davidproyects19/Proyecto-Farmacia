using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PrjFarmacia_David.Models
{
    public partial class FARMACIABDContext : DbContext
    {
        public FARMACIABDContext()
        {
        }

        public FARMACIABDContext(DbContextOptions<FARMACIABDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<Vendedor> Vendedors { get; set; } = null!;
        public virtual DbSet<VentasCab> VentasCabs { get; set; } = null!;
        public virtual DbSet<VentasDetum> VentasDeta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=DAVID;database=FARMACIABD;integrated security=true;");
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Modern_Spanish_CI_AI");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.EliCli)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("eli_cli")
                    .HasDefaultValueSql("('No')")
                    .IsFixedLength();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Descripcion)
                    .HasColumnType("text")
                    .HasColumnName("descripcion");

                entity.Property(e => e.EliPro)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("eli_pro")
                    .HasDefaultValueSql("('No')")
                    .IsFixedLength();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.login_usu)
                    .HasName("PK__Usuarios__0EA877E0D60E05AF");

                entity.Property(e => e.login_usu)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("login_usu");

                entity.Property(e => e.clave_usu)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("clave_usu");
        
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.HasKey(e => e.CodVen)
                    .HasName("PK__Vendedor__F2085E30C840B7F3");

                entity.ToTable("Vendedor");

                entity.Property(e => e.CodVen).HasColumnName("cod_ven");

                entity.Property(e => e.FecingVen)
                    .HasColumnType("date")
                    .HasColumnName("fecing_ven");

                entity.Property(e => e.NomVen)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nom_ven");
            });

            modelBuilder.Entity<VentasCab>(entity =>
            {
                entity.HasKey(e => e.NumVta)
                    .HasName("PK__Ventas_C__C78E290BF5FAD6DA");

                entity.ToTable("Ventas_Cab");

                entity.Property(e => e.NumVta)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("num_vta")
                    .IsFixedLength();

                entity.Property(e => e.Anulado)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("anulado")
                    .HasDefaultValueSql("('No')")
                    .IsFixedLength();

                entity.Property(e => e.CodCli).HasColumnName("cod_cli");

                entity.Property(e => e.CodVen).HasColumnName("cod_ven");

                entity.Property(e => e.FecVta)
                    .HasColumnType("date")
                    .HasColumnName("fec_vta");

                entity.Property(e => e.TotVta)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("tot_vta")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CodCliNavigation)
                    .WithMany(p => p.VentasCabs)
                    .HasForeignKey(d => d.CodCli)
                    .HasConstraintName("fk_Ventas_Cab_cod_cli");

                entity.HasOne(d => d.CodVenNavigation)
                    .WithMany(p => p.VentasCabs)
                    .HasForeignKey(d => d.CodVen)
                    .HasConstraintName("fk_Ventas_Cab_cod_ven");
            });

            modelBuilder.Entity<VentasDetum>(entity =>
            {
                entity.HasKey(e => new { e.NumVta, e.CodPro })
                    .HasName("PK__Ventas_D__E8569D6F056130E2");

                entity.ToTable("Ventas_Deta");

                entity.Property(e => e.NumVta)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("num_vta")
                    .IsFixedLength();

                entity.Property(e => e.CodPro).HasColumnName("cod_pro");

                entity.Property(e => e.Anulado)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("anulado")
                    .HasDefaultValueSql("('No')")
                    .IsFixedLength();

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.CodProNavigation)
                    .WithMany(p => p.VentasDeta)
                    .HasForeignKey(d => d.CodPro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ventas_De__cod_p__300424B4");

                entity.HasOne(d => d.NumVtaNavigation)
                    .WithMany(p => p.VentasDeta)
                    .HasForeignKey(d => d.NumVta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ventas_De__num_v__2F10007B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
