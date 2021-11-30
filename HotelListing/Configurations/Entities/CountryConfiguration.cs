using HotelListing.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Configurations.Entities
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        //Country Seeding Configuration
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
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
        }
    }
}
