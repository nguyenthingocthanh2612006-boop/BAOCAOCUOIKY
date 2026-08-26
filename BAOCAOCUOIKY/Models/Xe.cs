namespace BAOCAOCUOIKY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Xe")]
    public partial class Xe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Xe()
        {
            ChuyenXes = new HashSet<ChuyenXe>();
            Ghes = new HashSet<Ghe>();
        }

        [Key]
        [StringLength(20)]
        public string MaXe { get; set; }

        [Required]
        [StringLength(20)]
        public string BienSo { get; set; }

        [StringLength(50)]
        public string HangXe { get; set; }

        public int? NamSanXuat { get; set; }

        [StringLength(30)]
        public string MauXe { get; set; }

        [StringLength(30)]
        public string TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChuyenXe> ChuyenXes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ghe> Ghes { get; set; }
    }
}
