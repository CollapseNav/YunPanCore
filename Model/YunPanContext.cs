using System;
using Microsoft.EntityFrameworkCore;

namespace Model
{
    public class YunPanContext : DbContext
    {

        public YunPanContext(DbContextOptions<YunPanContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDataInfo>(entity =>
            {
                entity.HasIndex(m => m.Id).IsUnique();
                entity.HasIndex(m => m.UserName).IsUnique();
                entity.HasIndex(m => m.UserAccount).IsUnique();
                entity.HasMany(m => m.UserFiles).WithOne(m => m.Owner).HasForeignKey(m => m.OwnerId);
            });

            modelBuilder.Entity<FileInfo>(entity =>
            {
                entity.HasIndex(m => m.Id).IsUnique();
                entity.HasOne(m => m.Owner).WithMany(m => m.UserFiles);
            });
        }

        public DbSet<UserDataInfo> UserDataInfos { get; set; }

        public DbSet<FileInfo> FileInfos { get; set; }

        public DbSet<SharedFileInfo> SharedFiles { get; set; }

    }
}
