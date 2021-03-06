using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace MusicianHub.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //added to let code first know about your new models
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        /// <summary>
        /// For Overriding Code-first-Conventions Using Fluent API
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //to disaple on delete cascade for  user/gig/attendance

            modelBuilder.Entity<Attendance>()
                .HasRequired(a => a.Gig)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.Followers)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.Followees)
                .WithRequired(f => f.Follower)
                .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }


    }
}