using System;
using System.Collections.Generic;
using Bankomat_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Bankomat_API.Context;

public partial class SoluzioneBankomatContext : DbContext
{
    public SoluzioneBankomatContext()
    {
    }

    public SoluzioneBankomatContext(DbContextOptions<SoluzioneBankomatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Banche> Banches { get; set; }

    public virtual DbSet<BancheFunzionalitum> BancheFunzionalita { get; set; }

    public virtual DbSet<ContiCorrente> ContiCorrentes { get; set; }

    public virtual DbSet<Funzionalitum> Funzionalita { get; set; }

    public virtual DbSet<Movimenti> Movimentis { get; set; }

    public virtual DbSet<Utenti> Utentis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("data source=.; initial catalog=soluzione_bankomat; User ID=sa; Password=password123; Trusted_Connection=true; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banche>(entity =>
        {
            entity.ToTable("Banche");

            entity.HasIndex(e => e.Nome, "IX_Banche").IsUnique();

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BancheFunzionalitum>(entity =>
        {
            entity.ToTable("Banche_Funzionalita");

            entity.HasIndex(e => new { e.IdBanca, e.IdFunzionalita }, "IX_BancheFunzionalita").IsUnique();

            entity.HasOne(d => d.IdBancaNavigation).WithMany(p => p.BancheFunzionalita)
                .HasForeignKey(d => d.IdBanca)
                .HasConstraintName("FK_Banche_Funzionalita_Banche");

            entity.HasOne(d => d.IdFunzionalitaNavigation).WithMany(p => p.BancheFunzionalita)
                .HasForeignKey(d => d.IdFunzionalita)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Banche_Funzionalita_Funzionalita");
        });

        modelBuilder.Entity<ContiCorrente>(entity =>
        {
            entity.ToTable("ContiCorrente");

            entity.Property(e => e.DataUltimaOperazione).HasColumnType("date");
            entity.Property(e => e.DataUltimoVersamento).HasColumnType("date");

            entity.HasOne(d => d.IdUtenteNavigation).WithMany(p => p.ContiCorrentes)
                .HasForeignKey(d => d.IdUtente)
                .HasConstraintName("FK_ContiCorrente_Utenti");
        });

        modelBuilder.Entity<Funzionalitum>(entity =>
        {
            entity.HasIndex(e => e.Nome, "IX_Funzionalita").IsUnique();

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Movimenti>(entity =>
        {
            entity.ToTable("Movimenti");

            entity.Property(e => e.DataOperazione).HasColumnType("date");
            entity.Property(e => e.Funzionalita)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NomeBanca)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.NomeUtente)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Utenti>(entity =>
        {
            entity.ToTable("Utenti");

            entity.HasIndex(e => e.NomeUtente, "IX_Utenti").IsUnique();

            entity.Property(e => e.NomeUtente)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdBancaNavigation).WithMany(p => p.Utentis)
                .HasForeignKey(d => d.IdBanca)
                .HasConstraintName("FK_Utenti_Banche");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
