using MovieApp.Business.DTOs.TokenDTOs;
using MovieApp.Business.DTOs.UserDTOs;

namespace MovieApp.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task Register(UserRegisterDto userRegisterDto);
        Task<TokenResponseDto> Login(UserLoginDto userLoginDto);

    }
}
