using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Data
{

    // this custom user Type
    public class APIUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
