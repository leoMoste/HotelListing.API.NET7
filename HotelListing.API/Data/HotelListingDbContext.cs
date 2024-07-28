using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data
{
    public class HotelListingDbContext:DbContext
    {
        public HotelListingDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                new Country
                {
                    Id = 2,
                    Name = "Deutschland",
                    ShortName = "DE"
                },
                new Country
                {
                    Id = 3,
                    Name = "Englend",
                    ShortName = "EN"
                }
                );

           modelBuilder.Entity<Hotel>().HasData(
           new Hotel
           {
               Id = 1,
               Name = "Sandals Resort and SPA",
               Address = "Nergil",
               CountryId = 1,
               Rating = 4.5
               
           },
           new Hotel
           {
               Id = 2,
               Name = "Comfort Suites",
               Address = "Berlin",
               CountryId = 2,
               Rating = 4.3
           },
           new Hotel
           {
               Id = 3,
               Name = "Grand Palldium",
               Address = "London",
               CountryId = 3,
               Rating = 4
           }
           );
        }
    }
}
