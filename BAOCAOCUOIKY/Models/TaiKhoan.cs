namespace BAOCAOCUOIKY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [Key]
        [StringLength(20)]
        public string MaTK { get; set; }

        [StringLength(20)]
        public string MaNV { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(255)]
        public string MatKhau { get; set; }

        [Required]
        [StringLength(30)]
        public string Quyen { get; set; }

        [StringLength(30)]
        public string TrangThai { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
