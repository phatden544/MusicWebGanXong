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
        [Key]
        public int idbaihat { get; set; }

        public int? idPlaylist { get; set; }

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

        public virtual Playlist Playlist { get; set; }

        public virtual TheLoai TheLoai { get; set; }
        [InverseProperty("playlist")]
        public virtual ICollection<BaiHat> BaiHats { get; set; }




    }
}
