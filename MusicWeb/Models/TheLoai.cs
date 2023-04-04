namespace MusicWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TheLoai")]
    public partial class TheLoai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TheLoai()
        {
            BaiHats = new HashSet<BaiHat>();
        }

        [Key]
        [StringLength(10)]
        public string idtheloai { get; set; }

        [StringLength(10)]
        public string idchude { get; set; }

        [StringLength(50)]
        public string tentheloai { get; set; }

        [StringLength(10)]
        public string hinhtheloai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiHat> BaiHats { get; set; }

        public virtual ChuDe ChuDe { get; set; }
    }
}
