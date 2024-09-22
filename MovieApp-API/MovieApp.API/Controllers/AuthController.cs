using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.DTOs.TokenDTOs;
using MovieApp.Business.DTOs.UserDTOs;
using MovieApp.Business.Services.Interfaces;
using MovieApp.Core.Entities;

namespace MovieApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IAuthService authService;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.authService = authService;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            try
            {
                await authService.Register(userRegisterDto);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            return Ok();
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            TokenResponseDto data = null;
            try
            {
                data = await authService.Login(userLoginDto);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            return Ok(data);
        }

    }
}

