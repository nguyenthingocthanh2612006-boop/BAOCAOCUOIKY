namespace BAOCAOCUOIKY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BenXe")]
    public partial class BenXe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BenXe()
        {
            TuyenXes = new HashSet<TuyenXe>();
            TuyenXes1 = new HashSet<TuyenXe>();
        }

        [Key]
        [StringLength(20)]
        public string MaBenXe { get; set; }

        [Required]
        [StringLength(100)]
        public string TenBenXe { get; set; }

        [Required]
        [StringLength(255)]
        public string DiaChi { get; set; }

        [StringLength(30)]
        public string TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TuyenXe> TuyenXes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TuyenXe> TuyenXes1 { get; set; }
    }
}
