namespace MusicWeb.Models
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
        public int idPlaylist { get; set; }

        [StringLength(100)]
        public string tenplaylist { get; set; }

        [StringLength(100)]
        public string hinh { get; set; }

        public virtual BaiHat BaiHat { get; set; }
        [ForeignKey("idPlaylist")]
        public virtual Playlist playlist { get; set; }
    }
}
