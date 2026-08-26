namespace BAOCAOCUOIKY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TuyenXe")]
    public partial class TuyenXe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TuyenXe()
        {
            ChuyenXes = new HashSet<ChuyenXe>();
        }

        [Key]
        [StringLength(20)]
        public string MaTuyen { get; set; }

        [Required]
        [StringLength(150)]
        public string TenTuyen { get; set; }

        [Required]
        [StringLength(20)]
        public string MaBenXeDi { get; set; }

        [Required]
        [StringLength(20)]
        public string MaBenXeDen { get; set; }

        public double? KhoangCach { get; set; }

        public int? ThoiGianDuKien { get; set; }

        public decimal GiaVeCoBan { get; set; }

        [StringLength(30)]
        public string TrangThai { get; set; }

        public virtual BenXe BenXe { get; set; }

        public virtual BenXe BenXe1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChuyenXe> ChuyenXes { get; set; }
    }
}
