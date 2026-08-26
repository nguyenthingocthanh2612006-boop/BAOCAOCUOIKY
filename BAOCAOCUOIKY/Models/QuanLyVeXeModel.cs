using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BAOCAOCUOIKY.Models
{
    public partial class QuanLyVeXeModel : DbContext
    {
        public QuanLyVeXeModel()
            : base("name=QuanLyVeXeModel")
        {
        }

        public virtual DbSet<BenXe> BenXes { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<ChuyenXe> ChuyenXes { get; set; }
        public virtual DbSet<Ghe> Ghes { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TaiXe> TaiXes { get; set; }
        public virtual DbSet<TuyenXe> TuyenXes { get; set; }
        public virtual DbSet<VeXe> VeXes { get; set; }
        public virtual DbSet<Xe> Xes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BenXe>()
                .Property(e => e.MaBenXe)
                .IsUnicode(false);

            modelBuilder.Entity<BenXe>()
                .HasMany(e => e.TuyenXes)
                .WithRequired(e => e.BenXe)
                .HasForeignKey(e => e.MaBenXeDen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BenXe>()
                .HasMany(e => e.TuyenXes1)
                .WithRequired(e => e.BenXe1)
                .HasForeignKey(e => e.MaBenXeDi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaHD)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.MaVe)
                .IsUnicode(false);

            modelBuilder.Entity<ChuyenXe>()
                .Property(e => e.MaChuyen)
                .IsUnicode(false);

            modelBuilder.Entity<ChuyenXe>()
                .Property(e => e.MaTuyen)
                .IsUnicode(false);

            modelBuilder.Entity<ChuyenXe>()
                .Property(e => e.MaXe)
                .IsUnicode(false);

            modelBuilder.Entity<ChuyenXe>()
                .Property(e => e.MaTX)
                .IsUnicode(false);

            modelBuilder.Entity<ChuyenXe>()
                .HasMany(e => e.VeXes)
                .WithRequired(e => e.ChuyenXe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ghe>()
                .Property(e => e.MaGhe)
                .IsUnicode(false);

            modelBuilder.Entity<Ghe>()
                .Property(e => e.MaXe)
                .IsUnicode(false);

            modelBuilder.Entity<Ghe>()
                .HasMany(e => e.VeXes)
                .WithRequired(e => e.Ghe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaHD)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.MaVe)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.CCCD)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.VeXes)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MaTK)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiXe>()
                .Property(e => e.MaTX)
                .IsUnicode(false);

            modelBuilder.Entity<TaiXe>()
                .Property(e => e.SoDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<TaiXe>()
                .Property(e => e.SoBangLai)
                .IsUnicode(false);

            modelBuilder.Entity<TaiXe>()
                .HasMany(e => e.ChuyenXes)
                .WithRequired(e => e.TaiXe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TuyenXe>()
                .Property(e => e.MaTuyen)
                .IsUnicode(false);

            modelBuilder.Entity<TuyenXe>()
                .Property(e => e.MaBenXeDi)
                .IsUnicode(false);

            modelBuilder.Entity<TuyenXe>()
                .Property(e => e.MaBenXeDen)
                .IsUnicode(false);

            modelBuilder.Entity<TuyenXe>()
                .HasMany(e => e.ChuyenXes)
                .WithRequired(e => e.TuyenXe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VeXe>()
                .Property(e => e.MaVe)
                .IsUnicode(false);

            modelBuilder.Entity<VeXe>()
                .Property(e => e.MaChuyen)
                .IsUnicode(false);

            modelBuilder.Entity<VeXe>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<VeXe>()
                .Property(e => e.MaGhe)
                .IsUnicode(false);

            modelBuilder.Entity<VeXe>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<VeXe>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.VeXe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VeXe>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.VeXe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Xe>()
                .Property(e => e.MaXe)
                .IsUnicode(false);

            modelBuilder.Entity<Xe>()
                .Property(e => e.BienSo)
                .IsUnicode(false);

            modelBuilder.Entity<Xe>()
                .HasMany(e => e.ChuyenXes)
                .WithRequired(e => e.Xe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Xe>()
                .HasMany(e => e.Ghes)
                .WithRequired(e => e.Xe)
                .WillCascadeOnDelete(false);
        }
    }
}
