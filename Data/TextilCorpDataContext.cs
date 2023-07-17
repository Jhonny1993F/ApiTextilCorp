using System;
using System.Collections.Generic;
using ApiTextilCorp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTextilCorp.Data;

public partial class TextilCorpDataContext : DbContext
{
    public TextilCorpDataContext()
    {
    }

    public TextilCorpDataContext(DbContextOptions<TextilCorpDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TextilCorp.Data");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.ToTable("Carrito");

            entity.HasIndex(e => e.ClientesId, "IX_Carrito_ClientesId");

            entity.HasIndex(e => e.ProductosId, "IX_Carrito_ProductosId");

            entity.HasOne(d => d.Clientes).WithMany(p => p.Carritos).HasForeignKey(d => d.ClientesId);

            entity.HasOne(d => d.Productos).WithMany(p => p.Carritos).HasForeignKey(d => d.ProductosId);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriasId);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClientesId);
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.MarcasId);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductosId);

            entity.HasIndex(e => e.CategoriasId, "IX_Productos_CategoriasId");

            entity.HasIndex(e => e.MarcasId, "IX_Productos_MarcasId");

            entity.HasOne(d => d.Categorias).WithMany(p => p.Productos).HasForeignKey(d => d.CategoriasId);

            entity.HasOne(d => d.Marcas).WithMany(p => p.Productos).HasForeignKey(d => d.MarcasId);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuariosId);
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.VentasId);

            entity.HasIndex(e => e.ClientesId, "IX_Ventas_ClientesId");

            entity.HasIndex(e => e.ProductosId, "IX_Ventas_ProductosId");

            entity.HasOne(d => d.Clientes).WithMany(p => p.Venta).HasForeignKey(d => d.ClientesId);

            entity.HasOne(d => d.Productos).WithMany(p => p.Venta).HasForeignKey(d => d.ProductosId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
