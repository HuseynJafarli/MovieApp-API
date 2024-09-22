using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Business.DTOs.TokenDTOs;
using MovieApp.Business.DTOs.UserDTOs;
using MovieApp.Business.Services.Interfaces;
using MovieApp.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieApp.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<TokenResponseDto> Login(UserLoginDto userLoginDto)
        {
            AppUser user = null;
            user = await _userManager.FindByNameAsync(userLoginDto.Username);
            if (user == null)
            {
                throw new NullReferenceException("Invalid Credentials");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe);
            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Fullname", user.FullName),
                .. roles.Select(role=>new Claim(ClaimTypes.Role, role))
            ];

            string secretKey = "e2a4d435-f1e2-4dc3-bb95-88aa7abbe51c";
            DateTime expires = DateTime.UtcNow.AddDays(1);

            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new(
                signingCredentials: signingCredentials,
                claims: claims,
                audience: "http://localhost:5267/",
                issuer: "http://localhost:5267/",
                expires: expires,
                notBefore: DateTime.UtcNow
                );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new TokenResponseDto(token, expires);
        }

        public async Task Register(UserRegisterDto userRegisterDto)
        {
            AppUser appUser = new AppUser()
            {
                Email = userRegisterDto.Email,
                FullName = userRegisterDto.Fullname,
                UserName = userRegisterDto.Username
            };

            await _userManager.CreateAsync(appUser, userRegisterDto.Password);
        }
    }
}
