using AutoMapper;
using HotelListing.API.Contacts;
using HotelListing.API.Data;
using HotelListing.API.DTO.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelListing.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<APIUser> _userManager;
        private readonly IConfiguration _configuration;
        private APIUser _user;

        private const string _loginProvider = "HotelListingAPI";
        private const string _refreshToken = "RefreshToken";

        public AuthManager(IMapper mapper, UserManager<APIUser> userManager, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._configuration = configuration;
        }

        public async Task<string> CreateRefreshToken()
        {
            // this will remove the old Token from the DB
            await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);

            // now will genrate a new one
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);

            var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);

            return newRefreshToken;

        }


        public async Task<AuthResponseDto> Login(LoginDTO loginDto)
        {

            _user = await _userManager.FindByEmailAsync(loginDto.Email);
           bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);

            if(_user == null || isValidUser == false)
            {
                return null;
            }

            var token = await GenerateToken();
            return new AuthResponseDto
            {
                Token = token,
                UserId = _user.Id,
                RefreshToken = await CreateRefreshToken()
            };

        }

        public async Task<IEnumerable<IdentityError>> Register(APIUserDTO userDTO)
        {
            // No need to use _context directly here because "UserManager"
            // handles all database operations for IdentityUser internally.
            // we do not extra _context calling to use the DB


            // firtstlly mapping between the APIUser AND userDTO
            _user = _mapper.Map<APIUser>(userDTO);
            _user.UserName = userDTO.Email; // because our Email in DTO is kind of uniqe

            var result = await _userManager.CreateAsync(_user, userDTO.Password); // after it makes this call, it is automatically going to say let me encrypt this

            // add role to the User
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, "User");
            }

           return result.Errors;
        }

        public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
        {
            var jwtSecurityTokenHandler =  new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);

            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = await _userManager.FindByNameAsync(username);
        
            if (_user == null || _user.Id != request.UserId)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await GenerateToken();
                return new AuthResponseDto
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await _userManager.UpdateSecurityStampAsync(_user);
            return null;

        }

        private async Task<string> GenerateToken()
        {
            //signing Credentials
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
       
            var roles = await _userManager.GetRolesAsync(_user); // here saying give me the roles that this user has it in DB

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            /*
             * both code snippets achieve the same goal: they retrieve the roles associated with a user from the database and add 
             * those roles as claims to a collection of claims.
             var roles = await _userManager.GetRolesAsync(user);
              foreach (var role in roles) 
              {
                    claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
               }

             */

            // if there were any claims in the DB that we would want,
            // we could actually go and fetch them
            var userClaims = await _userManager.GetClaimsAsync(_user);


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("uid", _user.Id),
            }.Union(userClaims).Union(roleClaims);


            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWT:DurationInMinutes"])),
                signingCredentials: credentials
                );


            return new JwtSecurityTokenHandler().WriteToken(token); // this will return a string represent our Token

        }

       
    }
}
