namespace BAOCAOCUOIKY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VeXe")]
    public partial class VeXe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VeXe()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        [StringLength(20)]
        public string MaVe { get; set; }

        [Required]
        [StringLength(20)]
        public string MaChuyen { get; set; }

        [Required]
        [StringLength(20)]
        public string MaKH { get; set; }

        [Required]
        [StringLength(20)]
        public string MaGhe { get; set; }

        [StringLength(20)]
        public string MaNV { get; set; }

        [StringLength(30)]
        public string PhuongThucThanhToan { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayDat { get; set; }

        public TimeSpan ThoiGianDat { get; set; }

        public decimal GiaVe { get; set; }

        [StringLength(30)]
        public string TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual ChuyenXe ChuyenXe { get; set; }

        public virtual Ghe Ghe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
