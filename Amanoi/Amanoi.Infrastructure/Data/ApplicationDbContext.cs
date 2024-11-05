using Microsoft.EntityFrameworkCore;
using Amanoi.Domain.Entities;

namespace Amanoi.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Villa> Villas { get; set; } // Thêm DbSet cho model Villa
        public DbSet<VillaNumber> VillaNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {// Phương thức OnModelCreating để thêm seed data
            base.OnModelCreating(modelBuilder);

            // Define the relationship between VillaNumber and Villa
            modelBuilder.Entity<VillaNumber>()
                .HasOne(vn => vn.Villa)
                .WithMany(v => v.VillaNumbers) // Assuming Villa has a collection property VillaNumbers
                .HasForeignKey(vn => vn.VillaId);

            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x400",
                    Occupancy = 4,
                    Price = 200,
                    Sqft = 550,
                },
                new Villa
                {
                    Id = 2,
                    Name = "Premium Pool Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x401",
                    Occupancy = 4,
                    Price = 300,
                    Sqft = 550,
                },
                new Villa
                {
                    Id = 3,
                    Name = "Luxury Pool Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x402",
                    Occupancy = 4,
                    Price = 400,
                    Sqft = 750,
                }
            );


            modelBuilder.Entity<VillaNumber>().HasData(
             new VillaNumber
             {
                 Villa_Number = 101,
                 VillaId = 1,
             },
            new VillaNumber
            {
                Villa_Number = 102,
                VillaId = 1,
            },
            new VillaNumber
            {
                Villa_Number = 103,
                VillaId = 1,
            },
            new VillaNumber
            {
                Villa_Number = 104,
                VillaId = 1,
            },
            new VillaNumber
            {
                Villa_Number = 201,
                VillaId = 2,
            },
            new VillaNumber
            {
                Villa_Number = 202,
                VillaId = 2,
            },
            new VillaNumber
            {
                Villa_Number = 203,
                VillaId = 2,
            },
            new VillaNumber
            {
                Villa_Number = 301,
                VillaId = 3,
            },
            new VillaNumber
            {
                Villa_Number = 302,
                VillaId = 3,
            }
            );
        }
    }
}