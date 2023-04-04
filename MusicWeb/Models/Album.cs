namespace MusicWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Album")]
    public partial class Album
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album()
        {
            BaiHats = new HashSet<BaiHat>();
        }

        [Key]
        [StringLength(10)]
        public string idAlbum { get; set; }

        [StringLength(50)]
        public string TenAlbum { get; set; }

        [StringLength(50)]
        public string Tencasi { get; set; }

        public virtual Casi Casi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiHat> BaiHats { get; set; }
    }
}
