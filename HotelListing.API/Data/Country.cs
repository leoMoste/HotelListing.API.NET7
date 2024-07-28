namespace HotelListing.API.Data
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ShortName { get; set; }

        // Navigation Property: This indicates that one country can have many hotels
        public virtual IList<Hotel> Hotels { get; set; }  // Navigation property


    }
}