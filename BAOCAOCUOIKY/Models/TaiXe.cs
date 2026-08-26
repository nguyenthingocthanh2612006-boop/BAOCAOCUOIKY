namespace BAOCAOCUOIKY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiXe")]
    public partial class TaiXe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiXe()
        {
            ChuyenXes = new HashSet<ChuyenXe>();
        }

        [Key]
        [StringLength(20)]
        public string MaTX { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        public string GioiTinh { get; set; }

        [Required]
        [StringLength(15)]
        public string SoDienThoai { get; set; }

        [Required]
        [StringLength(30)]
        public string SoBangLai { get; set; }

        [Required]
        [StringLength(10)]
        public string HangBang { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayCap { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayHetHan { get; set; }

        [StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(30)]
        public string TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChuyenXe> ChuyenXes { get; set; }
    }
}
