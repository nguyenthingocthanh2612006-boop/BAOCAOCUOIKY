namespace BAOCAOCUOIKY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuyenXe")]
    public partial class ChuyenXe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuyenXe()
        {
            VeXes = new HashSet<VeXe>();
        }

        [Key]
        [StringLength(20)]
        public string MaChuyen { get; set; }

        [Required]
        [StringLength(20)]
        public string MaTuyen { get; set; }

        [Required]
        [StringLength(20)]
        public string MaXe { get; set; }

        [Required]
        [StringLength(20)]
        public string MaTX { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayKhoiHanh { get; set; }

        public TimeSpan GioKhoiHanh { get; set; }

        public TimeSpan? GioDenDuKien { get; set; }

        public decimal GiaVe { get; set; }

        public int SoLuongVe { get; set; }

        [StringLength(30)]
        public string TrangThai { get; set; }

        public virtual TaiXe TaiXe { get; set; }

        public virtual TuyenXe TuyenXe { get; set; }

        public virtual Xe Xe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VeXe> VeXes { get; set; }
    }
}
