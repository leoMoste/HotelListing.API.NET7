using AutoMapper;
using HotelListing.API.Data;
using HotelListing.API.DTO.Country;
using HotelListing.API.DTO.Hotel;
using HotelListing.API.DTO.User;

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

            // Hotel
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();

            // User
            CreateMap<APIUser, APIUserDTO>().ReverseMap();
        }
    }
}
