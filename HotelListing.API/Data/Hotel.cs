using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Data
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public double Rating { get; set; }


        // This establishes the link between a hotel and its associated country.
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }  // Foreign key

        // This allows access to the country details from a hotel.
        public Country Country { get; set; }  // Navigation property

    }
}
