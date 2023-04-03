namespace MusicWeb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuDe")]
    public partial class ChuDe
    {
        [Key]
        [StringLength(10)]
        public string idchude { get; set; }

        [StringLength(50)]
        public string Tenchude { get; set; }

        [StringLength(50)]
        public string Hinhchude { get; set; }

        public virtual TheLoai TheLoai { get; set; }
    }
}
