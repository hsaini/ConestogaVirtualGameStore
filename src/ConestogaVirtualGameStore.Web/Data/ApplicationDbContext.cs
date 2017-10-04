namespace ConestogaVirtualGameStore.Web.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Presentation.Data.Configuration;
    using Presentation.Models;
    using Web.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new GameConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
