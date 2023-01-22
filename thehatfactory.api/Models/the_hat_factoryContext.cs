using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace thehatfactory.api.Models
{
    public partial class the_hat_factoryContext : DbContext
    {
        public the_hat_factoryContext()
        {
        }

        public the_hat_factoryContext(DbContextOptions<the_hat_factoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<FavoritosUsuario> FavoritosUsuarios { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<ProductoCompra> ProductoCompras { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("workstation id=thehatfactory.mssql.somee.com;packet size=4096;user id=estefano97_SQLLogin_1;pwd=71dl87jihh;data source=thehatfactory.mssql.somee.com;persist security info=False;initial catalog=thehatfactory");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compra>(entity =>
            {
                entity.ToTable("compras");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.FechaCompra)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_compra");

                entity.Property(e => e.IdPaypal)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("id_paypal");

                entity.Property(e => e.IdUsuarioCompra).HasColumnName("id_usuario_compra");

                entity.Property(e => e.TotalValue)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("total_value");

                entity.HasOne(d => d.IdUsuarioCompraNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdUsuarioCompra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__compras__id_usua__37A5467C");
            });

            modelBuilder.Entity<FavoritosUsuario>(entity =>
            {
                entity.ToTable("favoritos_usuarios");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.FavoritosUsuarios)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favoritos_usuarios_FK");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.FavoritosUsuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favoritos_usuarios_FK_1");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("productos");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Categoria).HasColumnName("categoria");

                entity.Property(e => e.Descuento)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("descuento");

                entity.Property(e => e.Equipo)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("equipo");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("imageURL");

                entity.Property(e => e.Liga)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("liga");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("precio");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("productName");
            });

            modelBuilder.Entity<ProductoCompra>(entity =>
            {
                entity.ToTable("producto_compra");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CompraId).HasColumnName("compra_id");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(15, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.ProductTalla)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("product_talla");

                entity.Property(e => e.ProductoId).HasColumnName("producto_id");

                entity.HasOne(d => d.Compra)
                    .WithMany(p => p.ProductoCompras)
                    .HasForeignKey(d => d.CompraId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("producto_compra_FK_1");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.ProductoCompras)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("producto_compra_FK");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.NombreCompleto)
                    .IsUnicode(false)
                    .HasColumnName("nombre_completo");

                entity.Property(e => e.Telefono).HasColumnName("telefono");

                entity.Property(e => e.UserPassword).HasColumnName("user_password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
