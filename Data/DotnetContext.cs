using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinal.NET.Data;

public partial class DotnetContext : DbContext
{
    public DotnetContext()
    {
    }

    public DotnetContext(DbContextOptions<DotnetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ChiTietHd> ChiTietHds { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<TrangThai> TrangThais { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-MUNPQ5U\\SQLEXPRESS;Database=dotnet;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.IdBrand).HasName("PK__Brand__662A665999DF2AB5");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandName).HasMaxLength(255);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCate).HasName("PK__Category__3B7B64020741E302");

            entity.ToTable("Category");

            entity.Property(e => e.CateName).HasMaxLength(255);
        });

        modelBuilder.Entity<ChiTietHd>(entity =>
        {
            entity.HasKey(e => e.MaCt).HasName("PK__ChiTietH__27258E74CAB08509");

            entity.ToTable("ChiTietHD");

            entity.Property(e => e.MaCt).HasColumnName("MaCT");
            entity.Property(e => e.MaHd).HasColumnName("MaHD");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.ChiTietHds)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHD__IdPro__10566F31");

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.ChiTietHds)
                .HasForeignKey(d => d.MaHd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHD__MaHD__0F624AF8");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHd).HasName("PK__HoaDon__2725A6E0E57E3C63");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.CachThanhToan).HasMaxLength(50);
            entity.Property(e => e.CachVanChuyen).HasMaxLength(50);
            entity.Property(e => e.DiaChi).HasMaxLength(60);
            entity.Property(e => e.DienThoai).HasMaxLength(24);
            entity.Property(e => e.GhiChu).HasMaxLength(50);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayCan).HasColumnType("datetime");
            entity.Property(e => e.NgayDat).HasColumnType("datetime");
            entity.Property(e => e.NgayGiao).HasColumnType("datetime");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK__HoaDon__MaNV__0C85DE4D");

            entity.HasOne(d => d.MaTrangThaiNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaTrangThai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__MaTrangT__0B91BA14");

            entity.HasOne(d => d.User).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__UserId__0A9D95DB");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70AAE9DB6B3");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .HasColumnName("MaNV");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.MatKhau).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__Product__2E8946D467CD56E1");

            entity.ToTable("Product");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.NameProduct).HasMaxLength(255);

            entity.HasOne(d => d.IdBrandNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdBrand)
                .HasConstraintName("FK__Product__IdBrand__5535A963");

            entity.HasOne(d => d.IdCateNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCate)
                .HasConstraintName("FK__Product__IdCate__5629CD9C");
        });

        modelBuilder.Entity<TrangThai>(entity =>
        {
            entity.HasKey(e => e.MaTrangThai).HasName("PK__TrangTha__AADE4138B66BFD71");

            entity.ToTable("TrangThai");

            entity.Property(e => e.MaTrangThai).ValueGeneratedNever();
            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.TenTrangThai).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C2C368C14");

            entity.ToTable("User");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.IsAdmin)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isAdmin");
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
