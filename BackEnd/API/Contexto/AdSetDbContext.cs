using System;
using System.Collections.Generic;
using API.Entidades;
using Microsoft.EntityFrameworkCore;

namespace API.Contexto;

public partial class AdSetDbContext : DbContext
{
    public AdSetDbContext()
    {
    }

    public AdSetDbContext(DbContextOptions<AdSetDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Foto> Foto { get; set; }

    public virtual DbSet<Opcional> Opcional { get; set; }

    public virtual DbSet<Pacote> Pacote { get; set; }

    public virtual DbSet<Portal> Portal { get; set; }

    public virtual DbSet<RelacaoVeiculoOpcional> RelacaoVeiculoOpcional { get; set; }

    public virtual DbSet<RelacaoVeiculoPacotePortal> RelacaoVeiculoPacotePortal { get; set; }

    public virtual DbSet<Veiculo> Veiculo { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Foto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Foto__3214EC27478B8C5E");
        });

        modelBuilder.Entity<Opcional>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Opcional__3214EC27101B7AB8");
        });

        modelBuilder.Entity<Pacote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pacote__3214EC278F1A74BF");

            entity.HasOne(d => d.Portal).WithMany(p => p.Pacote)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pacote_Portal");
        });

        modelBuilder.Entity<Portal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Portal__3214EC27AF26D932");
        });

        modelBuilder.Entity<RelacaoVeiculoOpcional>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RelacaoV__3214EC276129C2AB");

            entity.HasOne(d => d.Opcional).WithMany(p => p.RelacaoVeiculoOpcional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RelacaoVeiculoOpcional_Opcional");

            entity.HasOne(d => d.Veiculo).WithMany(p => p.RelacaoVeiculoOpcional)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RelacaoVeiculoOpcional_Veiculo");
        });

        modelBuilder.Entity<RelacaoVeiculoPacotePortal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RelacaoV__3214EC27769DCE51");

            entity.HasOne(d => d.Pacote).WithMany(p => p.RelacaoVeiculoPacotePortal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RelacaoVeiculoPacotePortal_Pacote");

            entity.HasOne(d => d.Portal).WithMany(p => p.RelacaoVeiculoPacotePortal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RelacaoVeiculoPacotePortal_Portal");

            entity.HasOne(d => d.Veiculo).WithMany(p => p.RelacaoVeiculoPacotePortal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RelacaoVeiculoPacotePortal_Veiculo");
        });

        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Veiculo__3214EC27C8994EF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
