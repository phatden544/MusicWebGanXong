using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MusicWeb.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Models")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<BaiHat> BaiHats { get; set; }
        public virtual DbSet<Casi> Casis { get; set; }
        public virtual DbSet<ChuDe> ChuDes { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TheLoai> TheLoais { get; set; }
        public virtual DbSet<chitiet_Playlist> chitiet_Playlist { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .Property(e => e.idAlbum)
                .IsFixedLength();

            modelBuilder.Entity<Album>()
                .HasMany(e => e.BaiHats)
                .WithOptional(e => e.Album)
                .HasForeignKey(e => e.idtheloai);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<BaiHat>()
                .Property(e => e.idAlbum)
                .IsFixedLength();

            modelBuilder.Entity<BaiHat>()
                .Property(e => e.idtheloai)
                .IsFixedLength();

            modelBuilder.Entity<BaiHat>()
                .HasMany(e => e.chitiet_Playlist)
                .WithRequired(e => e.BaiHat)
                .HasForeignKey(e => e.idPlaylist)
                .WillCascadeOnDelete(false);

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
                .HasMany(e => e.chitiet_Playlist)
                .WithRequired(e => e.Playlist)
                .WillCascadeOnDelete(false);

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
