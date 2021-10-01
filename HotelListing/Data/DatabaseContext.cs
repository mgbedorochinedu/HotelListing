using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {}

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Nigeria",
                    ShortName = "NG"
                },
                new Country
                {
                    Id = 2,
                    Name = "United State",
                    ShortName = "US"
                },
                new Country
                {
                    Id = 3,
                    Name = "Canada",
                    ShortName = "CA"
                }
            );
            builder.Entity<Hotel>().HasData(
               new Hotel
               {
                   Id = 1,
                   Name = "Sandals Resort and Spa",
                   Address = "Negril",
                   CountryId = 1,
                   Rating = 3.5
               },
               new Hotel
               {
                   Id = 2,
                   Name = "Comfort Suite",
                   Address = "George Town",
                   CountryId = 3,
                   Rating = 5
               },
               new Hotel
               {
                   Id = 3,
                   Name = "Grand Dieu Hotel",
                   Address = "Nassua",
                   CountryId = 2,
                   Rating = 4.5
               }
           );
        }

    }
}
