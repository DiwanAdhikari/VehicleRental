using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleRentalSystem.Data;

namespace VehicleRentalSystem.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Category> Category { get; set; }

        public DbSet<VehicleOwner> VehicleOwner { get; set; }
        public DbSet<Renter> Renter { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Review> Review { get; set; }
    }
}
