﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
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