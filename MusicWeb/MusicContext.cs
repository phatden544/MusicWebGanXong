using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MusicWeb
{
    public partial class MusicContext : DbContext
    {
        public MusicContext()
            : base("name=MusicContext")
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<BaiHat> BaiHats { get; set; }
        public virtual DbSet<Casi> Casis { get; set; }
        public virtual DbSet<ChuDe> ChuDes { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<TheLoai> TheLoais { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .Property(e => e.idAlbum)
                .IsFixedLength();

            modelBuilder.Entity<Album>()
                .HasMany(e => e.BaiHats)
                .WithOptional(e => e.Album)
                .HasForeignKey(e => e.idtheloai);

            modelBuilder.Entity<BaiHat>()
                .Property(e => e.idbaihat)
                .IsFixedLength();

            modelBuilder.Entity<BaiHat>()
                .Property(e => e.idAlbum)
                .IsFixedLength();

            modelBuilder.Entity<BaiHat>()
                .Property(e => e.idtheloai)
                .IsFixedLength();

            modelBuilder.Entity<BaiHat>()
                .Property(e => e.idplaylist)
                .IsFixedLength();

            modelBuilder.Entity<Casi>()
                .Property(e => e.idAlbum)
                .IsFixedLength();

            modelBuilder.Entity<ChuDe>()
                .Property(e => e.idchude)
                .IsFixedLength();

            modelBuilder.Entity<ChuDe>()
                .HasOptional(e => e.TheLoai)
                .WithRequired(e => e.ChuDe);

            modelBuilder.Entity<Playlist>()
                .Property(e => e.idPlaylist)
                .IsFixedLength();

            modelBuilder.Entity<Playlist>()
                .HasOptional(e => e.BaiHat)
                .WithRequired(e => e.Playlist);

            modelBuilder.Entity<TheLoai>()
                .Property(e => e.idtheloai)
                .IsFixedLength();

            modelBuilder.Entity<TheLoai>()
                .Property(e => e.idchude)
                .IsFixedLength();

            modelBuilder.Entity<TheLoai>()
                .Property(e => e.hinhtheloai)
                .IsFixedLength();

            modelBuilder.Entity<TheLoai>()
                .HasMany(e => e.BaiHats)
                .WithOptional(e => e.TheLoai)
                .HasForeignKey(e => e.idAlbum);
        }
    }
}
