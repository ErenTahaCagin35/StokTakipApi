using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StokTakipApi.Models;

public partial class StokTakip1927Context : DbContext
{
    public StokTakip1927Context()
    {
    }

    public StokTakip1927Context(DbContextOptions<StokTakip1927Context> options)
        : base(options)
    {
    }

    public virtual DbSet<TblKategoriler> TblKategorilers { get; set; }

    public virtual DbSet<TblKullanicilar> TblKullanicilars { get; set; }

    public virtual DbSet<TblMusteriler> TblMusterilers { get; set; }

    public virtual DbSet<TblStokCiki> TblStokCikis { get; set; }

    public virtual DbSet<TblStokGiris> TblStokGirises { get; set; }

    public virtual DbSet<TblTedarikciler> TblTedarikcilers { get; set; }

    public virtual DbSet<TblUrunler> TblUrunlers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=WIN-SERVER\\SQLEXPRESS;Initial Catalog=stok_takip_1927;Encrypt=False;User Id=sa;password=admin.1");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblKategoriler>(entity =>
        {
            entity.HasKey(e => e.KategoriId).HasName("PK_TblKategori");

            entity.ToTable("TblKategoriler");

            entity.Property(e => e.KategoriAd).HasMaxLength(50);
        });

        modelBuilder.Entity<TblKullanicilar>(entity =>
        {
            entity.HasKey(e => e.KullaniciId);

            entity.ToTable("TblKullanicilar");

            entity.Property(e => e.KullaniciAdi).HasMaxLength(50);
            entity.Property(e => e.Parola).HasMaxLength(50);
        });

        modelBuilder.Entity<TblMusteriler>(entity =>
        {
            entity.HasKey(e => e.MusteriId);

            entity.ToTable("TblMusteriler");

            entity.Property(e => e.Adres).HasMaxLength(50);
            entity.Property(e => e.FirmaAdi).HasMaxLength(50);
            entity.Property(e => e.Mail).HasMaxLength(50);
            entity.Property(e => e.Telefon).HasMaxLength(13);
            entity.Property(e => e.YetkiliAdSoyad).HasMaxLength(50);
        });

        modelBuilder.Entity<TblStokCiki>(entity =>
        {
            entity.HasKey(e => e.IslemId);

            entity.Property(e => e.Tarih).HasColumnType("datetime");

            entity.HasOne(d => d.Musteri).WithMany(p => p.TblStokCikis)
                .HasForeignKey(d => d.MusteriId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblStokCikis_TblMusteriler");

            entity.HasOne(d => d.Personel).WithMany(p => p.TblStokCikis)
                .HasForeignKey(d => d.PersonelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblStokCikis_TblKullanicilar");

            entity.HasOne(d => d.Urun).WithMany(p => p.TblStokCikis)
                .HasForeignKey(d => d.UrunId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblStokCikis_TblUrunler");
        });

        modelBuilder.Entity<TblStokGiris>(entity =>
        {
            entity.HasKey(e => e.IslemId);

            entity.ToTable("TblStokGiris");

            entity.Property(e => e.Tarih).HasColumnType("datetime");

            entity.HasOne(d => d.Personel).WithMany(p => p.TblStokGirises)
                .HasForeignKey(d => d.PersonelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblStokGiris_TblKullanicilar");

            entity.HasOne(d => d.Tedarikci).WithMany(p => p.TblStokGirises)
                .HasForeignKey(d => d.TedarikciId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblStokGiris_TblTedarikciler");

            entity.HasOne(d => d.Urun).WithMany(p => p.TblStokGirises)
                .HasForeignKey(d => d.UrunId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblStokGiris_TblUrunler");
        });

        modelBuilder.Entity<TblTedarikciler>(entity =>
        {
            entity.HasKey(e => e.TedarikciId).HasName("PK_Table_1");

            entity.ToTable("TblTedarikciler");

            entity.Property(e => e.Adres).HasMaxLength(100);
            entity.Property(e => e.FirmaAdi).HasMaxLength(100);
            entity.Property(e => e.Mail).HasMaxLength(50);
            entity.Property(e => e.Tel).HasMaxLength(13);
            entity.Property(e => e.YetkiliAdSoyad).HasMaxLength(50);
        });

        modelBuilder.Entity<TblUrunler>(entity =>
        {
            entity.HasKey(e => e.UrunId);

            entity.ToTable("TblUrunler");

            entity.Property(e => e.UrunAciklama).HasMaxLength(200);
            entity.Property(e => e.UrunAd).HasMaxLength(50);
            entity.Property(e => e.UrunKod).HasMaxLength(10);

            entity.HasOne(d => d.Kategori).WithMany(p => p.TblUrunlers)
                .HasForeignKey(d => d.KategoriId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblUrunler_TblKategoriler");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
