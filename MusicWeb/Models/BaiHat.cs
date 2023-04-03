namespace MusicWeb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiHat")]
    public partial class BaiHat
    {
        [Key]
        [StringLength(10)]
        public string idbaihat { get; set; }

        [StringLength(10)]
        public string idAlbum { get; set; }

        [StringLength(10)]
        public string idtheloai { get; set; }

        [StringLength(10)]
        public string idplaylist { get; set; }

        [StringLength(50)]
        public string Tenbaihat { get; set; }

        [StringLength(50)]
        public string Hinhbaihat { get; set; }

        [StringLength(50)]
        public string casi { get; set; }

        [StringLength(50)]
        public string linkbaihat { get; set; }

        public virtual Album Album { get; set; }

        public virtual Playlist Playlist { get; set; }

        public virtual TheLoai TheLoai { get; set; }
    }
}
