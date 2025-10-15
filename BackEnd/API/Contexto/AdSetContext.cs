using System;
using System.Collections.Generic;
using API.Entidades;
using Microsoft.EntityFrameworkCore;

namespace API.Contexto;

public partial class AdSetContext : DbContext
{
    public AdSetContext()
    {
    }

    public AdSetContext(DbContextOptions<AdSetContext> options)
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

            entity.HasOne(d => d.Veiculo).WithMany(p => p.Foto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Foto_Veiculo");
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
                .OnDelete(DeleteBehavior.Cascade)
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

        // SEED DATA PARA OPCIONAIS
        modelBuilder.Entity<Opcional>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Opcional__3214EC27101B7AB8");
            entity.HasData(
                new Opcional { Id = 1, Descricao = "Ar Condicionado" },
                new Opcional { Id = 2, Descricao = "Airbag" },
                new Opcional { Id = 3, Descricao = "Freios ABS" },
                new Opcional { Id = 4, Descricao = "Alarme" }
            );
        });

        // SEED DATA PARA PORTAIS (precisa vir antes de Pacote)
        modelBuilder.Entity<Portal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Portal__3214EC27AF26D932");
            entity.HasData(
                new Portal { Id = 1, Nome = "iCarros" },
                new Portal { Id = 2, Nome = "Webmotors" }
            );
        });

        // SEED DATA PARA PACOTES (com a chave estrangeira PortalId)
        modelBuilder.Entity<Pacote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pacote__3214EC278F1A74BF");
            entity.HasOne(d => d.Portal).WithMany(p => p.Pacote)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pacote_Portal");

            entity.HasData(
    
                new Pacote { Id = 1, Nome = "Básico", PortalId = 1 },
                new Pacote { Id = 2, Nome = "Bronze", PortalId = 1 },
                new Pacote { Id = 3, Nome = "Platinum", PortalId = 1 },
                new Pacote { Id = 4, Nome = "Diamante", PortalId = 1 },

                new Pacote { Id = 5, Nome = "Básico", PortalId = 2 },
                new Pacote { Id = 6, Nome = "Bronze", PortalId = 2 },
                new Pacote { Id = 7, Nome = "Platinum", PortalId = 2 },
                new Pacote { Id = 8, Nome = "Diamante", PortalId = 2 }


            );
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
