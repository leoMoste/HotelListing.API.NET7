﻿using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.DTO.Country
{
    public abstract class BaseCountryDTO
    {
        [Required]
        public string Name { get; set; }
        public string? ShortName { get; set; }
    }
}
