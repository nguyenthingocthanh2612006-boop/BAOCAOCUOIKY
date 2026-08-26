namespace BAOCAOCUOIKY.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ghe")]
    public partial class Ghe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ghe()
        {
            VeXes = new HashSet<VeXe>();
        }

        [Key]
        [StringLength(20)]
        public string MaGhe { get; set; }

        [Required]
        [StringLength(20)]
        public string MaXe { get; set; }

        public int SoGhe { get; set; }

        [StringLength(30)]
        public string TrangThai { get; set; }

        public virtual Xe Xe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VeXe> VeXes { get; set; }
    }
}
