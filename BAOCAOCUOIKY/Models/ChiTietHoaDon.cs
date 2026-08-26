namespace BAOCAOCUOIKY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MaVe { get; set; }

        public decimal DonGia { get; set; }

        public int? SoLuong { get; set; }

        public decimal ThanhTien { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual VeXe VeXe { get; set; }
    }
}
