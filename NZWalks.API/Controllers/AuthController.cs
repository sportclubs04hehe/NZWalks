using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        // REGISTER
        // POST: https://localhost:7010/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var idendityUser = new IdentityUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.UserName,
            };

            var identityResult = await _userManager.CreateAsync(idendityUser, registerDto.Password);

            if (identityResult.Succeeded)
            {
                // Add Roles to this User
                if (registerDto.Roles != null && registerDto.Roles.Any()) // Any return True or False if have a record
                {
                    identityResult = await _userManager.AddToRolesAsync(idendityUser, registerDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login");
                    }
                }
            }

            return BadRequest("Something went wrong!");
        }

        // LOGIN
        // POST: https://localhost:7010/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.UserName);

            if(user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                if(checkPasswordResult)
                {
                    // Get Roles for User
                    var roles = await _userManager.GetRolesAsync(user);

                    if(roles != null && roles.Any())
                    {
                        // Create Token
                        var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginReponseDto
                        {
                            JWTToken = jwtToken,
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or Password incorrect"); 
        }
    }
}
