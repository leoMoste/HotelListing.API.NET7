using HotelListing.API.DTO.Hotel;

namespace HotelListing.API.DTO.Country
{
    public class CountryDTO : BaseCountryDTO
    {
        public int Id { get; set; }
 
        public List<HotelDTO> Hotels { get; set; }
    }
}
