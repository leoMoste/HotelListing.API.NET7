using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.DTO.Country;
using HotelListing.API.DTO.Hotel;

namespace HotelListing.API.Configurations
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            // from Country to CreateCountryDTO and vice verca.
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Country, GetCountryDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, UpdateCountryDTO>().ReverseMap();

            CreateMap<Hotel, HotelDTO>().ReverseMap();
        
        }
    }
}
