namespace MusicWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiHat")]
    public partial class BaiHat
    {
        internal readonly object idplaylist;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiHat()
        {
            chitiet_Playlist = new HashSet<chitiet_Playlist>();
        }

        [Key]
        public int idbaihat { get; set; }

        [StringLength(10)]
        public string idAlbum { get; set; }

        [StringLength(10)]
        public string idtheloai { get; set; }

        [StringLength(50)]
        public string Tenbaihat { get; set; }

        [StringLength(50)]
        public string Hinhbaihat { get; set; }

        [StringLength(50)]
        public string casi { get; set; }

        [StringLength(50)]
        public string linkbaihat { get; set; }

        public virtual Album Album { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<chitiet_Playlist> chitiet_Playlist { get; set; }

        public virtual TheLoai TheLoai { get; set; }
    }
}
