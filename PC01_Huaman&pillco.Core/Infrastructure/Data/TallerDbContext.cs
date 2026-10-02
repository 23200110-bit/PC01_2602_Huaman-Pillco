using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PC01_Huaman_pillco.Core.Core.Entities;

namespace PC01_Huaman_pillco.Core.Infrastructure.Data;

public partial class TallerDbContext : DbContext
{
    public TallerDbContext(DbContextOptions<TallerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<OrdenServicio> OrdenServicios { get; set; }

    public virtual DbSet<TipoServicio> TipoServicios { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Cliente");

            entity.Property(e => e.Correo).HasMaxLength(150);
            entity.Property(e => e.Materno).HasMaxLength(50);
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.Paterno).HasMaxLength(50);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrdenServicio>(entity =>
        {
            entity.ToTable("OrdenServicio");

            entity.Property(e => e.CostoEstimado).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DescripcionProblema).HasMaxLength(1000);
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .HasDefaultValue("Pendiente", "DF_OrdenServicio_Estado");
            entity.Property(e => e.FechaIngreso)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_OrdenServicio_FechaIngreso");

            entity.HasOne(d => d.TipoServicio).WithMany(p => p.OrdenServicios)
                .HasForeignKey(d => d.TipoServicioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenServicio_TipoServicio");

            entity.HasOne(d => d.Vehiculo).WithMany(p => p.OrdenServicios)
                .HasForeignKey(d => d.VehiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenServicio_Vehiculo");
        });

        modelBuilder.Entity<TipoServicio>(entity =>
        {
            entity.ToTable("TipoServicio");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PrecioBase).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.ToTable("Vehiculo");

            entity.HasIndex(e => e.Placa, "UQ_Vehiculo_Placa").IsUnique();

            entity.Property(e => e.Marca).HasMaxLength(50);
            entity.Property(e => e.Modelo).HasMaxLength(50);
            entity.Property(e => e.Placa)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Cliente).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehiculo_Cliente");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
