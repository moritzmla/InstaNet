using InstaNet.ApplicationCore.Entities;
using InstaNet.DataAccess.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaNet.DataAccess.Data
{
    public class RepositoryContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Replay> Replays { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Follow> Follows { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(ConfigurateApplicationUser);
            modelBuilder.Entity<Profile>(ConfigurateProfile);
            modelBuilder.Entity<Post>(ConfiguratePost);
            modelBuilder.Entity<Picture>(ConfiguratePicture);
            modelBuilder.Entity<Video>(ConfigurateVideo);
            modelBuilder.Entity<Replay>(ConfigurateReplay);
        }

        private void ConfigurateApplicationUser(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(x => x.Profile)
                .WithOne();
        }

        private void ConfigurateProfile(EntityTypeBuilder<Profile> builder)
        {
            builder.Property(x => x.Created).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Modified).HasDefaultValue(DateTime.Now);

            builder.HasMany(x => x.Replays)
                .WithOne(x => x.Profile)
                .HasForeignKey(x => x.ProfileId);

            builder.HasMany(x => x.Likes)
                .WithOne(x => x.Profile)
                .HasForeignKey(x => x.ProfileId);

            builder.HasMany(x => x.Followers)
                .WithOne(x => x.Follower)
                .HasForeignKey(x => x.FollowerId);

            builder.HasMany(x => x.Following)
                .WithOne(x => x.Following)
                .HasForeignKey(x => x.FollowingId);

            builder.HasMany(x => x.Posts)
                .WithOne(x => x.Profile)
                .HasForeignKey(x => x.ProfileId);
        }

        private void ConfiguratePost(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Created).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Modified).HasDefaultValue(DateTime.Now);

            builder.HasMany(x => x.Pictures)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId);

            builder.HasMany(x => x.Likes)
               .WithOne(x => x.Post)
               .HasForeignKey(x => x.PostId);

            builder.HasMany(x => x.Videos)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId);

            builder.HasMany(x => x.Replays)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId);
        }


        private void ConfiguratePicture(EntityTypeBuilder<Picture> builder)
        {
            builder.Property(x => x.Created).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Modified).HasDefaultValue(DateTime.Now);
        }

        private void ConfigurateVideo(EntityTypeBuilder<Video> builder)
        {
            builder.Property(x => x.Created).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Modified).HasDefaultValue(DateTime.Now);
        }

        private void ConfigurateReplay(EntityTypeBuilder<Replay> builder)
        {
            builder.Property(x => x.Created).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Modified).HasDefaultValue(DateTime.Now);
        }
    }
}
