using HotelListing.API.Contacts;
using HotelListing.API.Data;

namespace HotelListing.API.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        private readonly HotelListingDbContext _context;
        public HotelRepository(HotelListingDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
