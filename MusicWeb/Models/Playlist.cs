namespace MusicWeb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Playlist")]
    public partial class Playlist
    {
        [Key]
        [StringLength(10)]
        public string idPlaylist { get; set; }

        [StringLength(50)]
        public string tenplaylist { get; set; }

        [StringLength(50)]
        public string hinh { get; set; }

        public virtual BaiHat BaiHat { get; set; }
    }
}
