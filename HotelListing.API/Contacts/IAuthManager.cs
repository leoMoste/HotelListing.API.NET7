using HotelListing.API.DTO.User;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contacts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(APIUserDTO userDTO);
        Task<AuthResponseDto> Login(LoginDTO userDTO);

        Task<string> CreateRefreshToken();
        Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
    }
}
