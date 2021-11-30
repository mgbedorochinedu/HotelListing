﻿using HotelListing.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Configurations.Entities
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        //Hotel Seeding Configuration
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
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
