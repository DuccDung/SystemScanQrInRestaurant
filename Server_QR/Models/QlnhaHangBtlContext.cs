﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Server_QR.Models;

public partial class QlnhaHangBtlContext : DbContext
{
    public QlnhaHangBtlContext()
    {
    }

    public QlnhaHangBtlContext(DbContextOptions<QlnhaHangBtlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ban> Bans { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }

    public virtual DbSet<CongThuc> CongThucs { get; set; }

    public virtual DbSet<DonHang> DonHangs { get; set; }

    public virtual DbSet<HoaDonNhap> HoaDonNhaps { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }

    public virtual DbSet<NgayCong> NgayCongs { get; set; }

    public virtual DbSet<NguyenLieu> NguyenLieus { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<NvNc> NvNcs { get; set; }

    public virtual DbSet<NvPq> NvPqs { get; set; }

    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCondition> ProductConditions { get; set; }

    public virtual DbSet<Thuong> Thuongs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ADMIN-PC\\MSSQLSERVER1;Initial Catalog=QLNhaHang_BTL;User ID=sa;Password=Dung@123;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ban>(entity =>
        {
            entity.HasKey(e => e.BanId).HasName("PK__Ban__FD9DEB6A34CDDB62");

            entity.ToTable("Ban");

            entity.Property(e => e.BanId).HasColumnName("Ban_ID");
            entity.Property(e => e.GhiChu)
                .HasMaxLength(50)
                .HasColumnName("Ghi Chu");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CateId).HasName("PK__Category__297787E653FAA53D");

            entity.ToTable("Category");

            entity.Property(e => e.CateId).HasColumnName("Cate_ID");
            entity.Property(e => e.MoTa).HasMaxLength(200);
            entity.Property(e => e.TenLoaiSanPham).HasMaxLength(100);
        });

        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => e.CthdId).HasName("PK__ChiTietH__74F653C0434FFCF5");

            entity.ToTable("ChiTietHoaDon");

            entity.Property(e => e.CthdId).HasColumnName("CTHD_ID");
            entity.Property(e => e.DhId).HasColumnName("DH_ID");
            entity.Property(e => e.Ghichu).HasMaxLength(500);
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.ThanhTien).HasColumnType("money");

            entity.HasOne(d => d.Dh).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.DhId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DonHang_DH_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ChiTietHo__Produ__5EBF139D");
        });

        modelBuilder.Entity<ChiTietHoaDonNhap>(entity =>
        {
            entity.HasKey(e => e.CthdnId).HasName("PK__ChiTietH__B652376B808EF55A");

            entity.ToTable("ChiTietHoaDonNhap");

            entity.Property(e => e.CthdnId).HasColumnName("CTHDN_ID");
            entity.Property(e => e.HdnId).HasColumnName("HDN_ID");
            entity.Property(e => e.NlId).HasColumnName("NL_ID");
            entity.Property(e => e.ThanhTien).HasColumnType("money");

            entity.HasOne(d => d.Hdn).WithMany(p => p.ChiTietHoaDonNhaps)
                .HasForeignKey(d => d.HdnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHo__HDN_I__60A75C0F");

            entity.HasOne(d => d.Nl).WithMany(p => p.ChiTietHoaDonNhaps)
                .HasForeignKey(d => d.NlId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHo__NL_ID__619B8048");
        });

        modelBuilder.Entity<CongThuc>(entity =>
        {
            entity.HasKey(e => e.CtId).HasName("PK__CongThuc__DC4F366B6628CB9B");

            entity.ToTable("CongThuc");

            entity.Property(e => e.CtId).HasColumnName("CT_ID");
            entity.Property(e => e.GhiChu).HasMaxLength(200);
            entity.Property(e => e.NlId).HasColumnName("NL_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.SoLuong).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Nl).WithMany(p => p.CongThucs)
                .HasForeignKey(d => d.NlId)
                .HasConstraintName("FK__CongThuc__NL_ID__628FA481");

            entity.HasOne(d => d.Product).WithMany(p => p.CongThucs)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__CongThuc__Produc__6383C8BA");
        });

        modelBuilder.Entity<DonHang>(entity =>
        {
            entity.HasKey(e => e.DhId).HasName("PK__DonHang__1A50AD039FAFCB39");

            entity.ToTable("DonHang");

            entity.Property(e => e.DhId).HasColumnName("DH_ID");
            entity.Property(e => e.BanId).HasColumnName("Ban_ID");
            entity.Property(e => e.GhiChu).HasMaxLength(500);
            entity.Property(e => e.GioRa).HasColumnType("datetime");
            entity.Property(e => e.GioVao).HasColumnType("datetime");
            entity.Property(e => e.KhId).HasColumnName("KH_ID");
            entity.Property(e => e.KmId).HasColumnName("KM_ID");
            entity.Property(e => e.NvId).HasColumnName("NV_ID");
            entity.Property(e => e.TongTien).HasColumnType("money");
            entity.Property(e => e.VanChuyen).HasDefaultValue(false);

            entity.HasOne(d => d.Ban).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.BanId)
                .HasConstraintName("FK__DonHang__Ban_ID__6477ECF3");

            entity.HasOne(d => d.Kh).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.KhId)
                .HasConstraintName("FK__DonHang__KH_ID__656C112C");

            entity.HasOne(d => d.Km).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.KmId)
                .HasConstraintName("FK__DonHang__KM_ID__66603565");

            entity.HasOne(d => d.Nv).WithMany(p => p.DonHangs)
                .HasForeignKey(d => d.NvId)
                .HasConstraintName("FK__DonHang__NV_ID__6754599E");
        });

        modelBuilder.Entity<HoaDonNhap>(entity =>
        {
            entity.HasKey(e => e.HdnId).HasName("PK__HoaDonNh__B1B553C955640900");

            entity.ToTable("HoaDonNhap");

            entity.Property(e => e.HdnId).HasColumnName("HDN_ID");
            entity.Property(e => e.NccId).HasColumnName("NCC_ID");
            entity.Property(e => e.NgayLapHoaDon).HasColumnType("datetime");
            entity.Property(e => e.NgayNhanHang).HasColumnType("datetime");
            entity.Property(e => e.NvId).HasColumnName("NV_ID");
            entity.Property(e => e.TongSoTien).HasColumnType("money");

            entity.HasOne(d => d.Ncc).WithMany(p => p.HoaDonNhaps)
                .HasForeignKey(d => d.NccId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDonNha__NCC_I__68487DD7");

            entity.HasOne(d => d.Nv).WithMany(p => p.HoaDonNhaps)
                .HasForeignKey(d => d.NvId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDonNha__NV_ID__693CA210");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.KhId).HasName("PK__KhachHan__2415FB21FE280C4F");

            entity.ToTable("KhachHang");

            entity.Property(e => e.KhId).HasColumnName("KH_ID");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.MatKhau).HasMaxLength(50);
            entity.Property(e => e.PathPhoto).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TaiKhoan).HasMaxLength(50);
            entity.Property(e => e.TenKhachHang).HasMaxLength(100);
        });

        modelBuilder.Entity<KhuyenMai>(entity =>
        {
            entity.HasKey(e => e.KmId).HasName("PK__KhuyenMa__2A8E390A51A35EA0");

            entity.ToTable("KhuyenMai");

            entity.Property(e => e.KmId).HasColumnName("KM_ID");
            entity.Property(e => e.GiamGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TenKhuyenMai).HasMaxLength(100);
        });

        modelBuilder.Entity<NgayCong>(entity =>
        {
            entity.HasKey(e => e.NcId).HasName("PK__NgayCong__53A08FCD63DF7EA3");

            entity.ToTable("NgayCong");

            entity.Property(e => e.NcId).HasColumnName("NC_ID");
            entity.Property(e => e.CaLam).HasMaxLength(50);
            entity.Property(e => e.NgayCong1).HasColumnName("NgayCong");
        });

        modelBuilder.Entity<NguyenLieu>(entity =>
        {
            entity.HasKey(e => e.NlId).HasName("PK__NguyenLi__07A8F6222AC30C15");

            entity.ToTable("NguyenLieu");

            entity.Property(e => e.NlId).HasColumnName("NL_ID");
            entity.Property(e => e.GiaTien).HasColumnType("money");
            entity.Property(e => e.TenNguyenLieu).HasMaxLength(100);
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.NccId).HasName("PK__NhaCungC__FD938E23C27C5E04");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.NccId)
                .ValueGeneratedNever()
                .HasColumnName("NCC_ID");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TenNhaCungCap).HasMaxLength(100);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.NvId).HasName("PK__NhanVien__E505EF9A6E4525C7");

            entity.ToTable("NhanVien");

            entity.Property(e => e.NvId).HasColumnName("NV_ID");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HeSoLuong).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MatKhau).HasMaxLength(50);
            entity.Property(e => e.PathPhoto).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TaiKhoan).HasMaxLength(50);
            entity.Property(e => e.TenNhanVien).HasMaxLength(100);
        });

        modelBuilder.Entity<NvNc>(entity =>
        {
            entity.HasKey(e => e.NvncId).HasName("PK__NV_NC__5D4B3292711FD0CD");

            entity.ToTable("NV_NC");

            entity.Property(e => e.NvncId).HasColumnName("NVNC_ID");
            entity.Property(e => e.NcId).HasColumnName("NC_ID");
            entity.Property(e => e.NvId).HasColumnName("NV_ID");

            entity.HasOne(d => d.Nc).WithMany(p => p.NvNcs)
                .HasForeignKey(d => d.NcId)
                .HasConstraintName("FK_NV_NC_NgayCong");

            entity.HasOne(d => d.Nv).WithMany(p => p.NvNcs)
                .HasForeignKey(d => d.NvId)
                .HasConstraintName("FK_NV_NC_NhanVien");
        });

        modelBuilder.Entity<NvPq>(entity =>
        {
            entity.HasKey(e => e.NvPqId).HasName("PK__NV_PQ__416F1F96CC65F9FC");

            entity.ToTable("NV_PQ");

            entity.Property(e => e.NvPqId).HasColumnName("NV_PQ_ID");
            entity.Property(e => e.MoTa).HasMaxLength(200);
            entity.Property(e => e.NvId).HasColumnName("NV_ID");
            entity.Property(e => e.PqId).HasColumnName("PQ_ID");

            entity.HasOne(d => d.Nv).WithMany(p => p.NvPqs)
                .HasForeignKey(d => d.NvId)
                .HasConstraintName("FK__NV_PQ__NV_ID__6C190EBB");

            entity.HasOne(d => d.Pq).WithMany(p => p.NvPqs)
                .HasForeignKey(d => d.PqId)
                .HasConstraintName("FK__NV_PQ__PQ_ID__6D0D32F4");
        });

        modelBuilder.Entity<PhanQuyen>(entity =>
        {
            entity.HasKey(e => e.PqId).HasName("PK__PhanQuye__50D8B482ED6EFCA9");

            entity.ToTable("PhanQuyen");

            entity.Property(e => e.PqId).HasColumnName("PQ_ID");
            entity.Property(e => e.TenQuyen).HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__9834FB9A6B459752");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.CateId).HasColumnName("Cate_ID");
            entity.Property(e => e.GiaTien).HasColumnType("money");
            entity.Property(e => e.MoTa).HasMaxLength(500);
            entity.Property(e => e.PathPhoto).HasMaxLength(300);
            entity.Property(e => e.TenSanPham).HasMaxLength(100);

            entity.HasOne(d => d.Cate).WithMany(p => p.Products)
                .HasForeignKey(d => d.CateId)
                .HasConstraintName("FK__Product__Cate_ID__6E01572D");
        });

        modelBuilder.Entity<ProductCondition>(entity =>
        {
            entity.HasKey(e => e.ProductConditionId).HasName("PK__ProductC__A289AD964FCEC8AD");

            entity.Property(e => e.ProductConditionId).HasColumnName("ProductCondition_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductConditions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductCondition_Product");
        });

        modelBuilder.Entity<Thuong>(entity =>
        {
            entity.HasKey(e => e.TId).HasName("PK__Thuong__83BB1FB206EC337C");

            entity.ToTable("Thuong");

            entity.Property(e => e.TId).HasColumnName("T_ID");
            entity.Property(e => e.NvId).HasColumnName("NV_ID");
            entity.Property(e => e.TenThuong).HasMaxLength(100);
            entity.Property(e => e.TienThuong).HasColumnType("money");

            entity.HasOne(d => d.Nv).WithMany(p => p.Thuongs)
                .HasForeignKey(d => d.NvId)
                .HasConstraintName("FK__Thuong__NV_ID__6FE99F9F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
