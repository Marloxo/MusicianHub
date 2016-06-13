using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace MusicianHub.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //added to let code first know about your new models
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}