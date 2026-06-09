using EFCoreLoading.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLoading
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Villa> Villas { get; set; }
        public DbSet<Models.VillaAmenity> VillaAmenities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Price = 200,
                },
                new Villa
                {
                    Id = 2,
                    Name = "Premium Pool Villa",
                    Price = 300,
                },
                new Villa
                {
                    Id = 3,
                    Name = "Luxury Pool Villa",
                    Price = 400,
                }
                );

            modelBuilder.Entity<VillaAmenity>().HasData(
                new VillaAmenity
                {
                    Id = 1,
                    VillaId = 1,
                    Name = "Private Pool",
                },
                new VillaAmenity
                {
                    Id = 2,
                    VillaId = 1,
                    Name = "Microwave",
                },
                new VillaAmenity
                {
                    Id = 3,
                    VillaId = 1,
                    Name = "Private Balcony",
                },
                new VillaAmenity
                {
                    Id = 4,
                    VillaId = 1,
                    Name = "1 king bed and 1 sofa bed",
                }
                );
        }
    }
}
