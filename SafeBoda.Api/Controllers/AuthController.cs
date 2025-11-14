using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SafeBoda.Core;

using SafeBoda.Authenticationkey;

namespace SafeBoda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    
    
    {
        
        
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
       
        private readonly JwtGenerator _jwtGenerator;

        public AuthController(IAuthService authService, JwtGenerator jwtGenerator, IConfiguration configuration, UserManager<User> userManager)
        {
            _authService = authService;
            _jwtGenerator = jwtGenerator;
            _configuration = configuration;
            _userManager = userManager;
        }

        // post: api/auth/register
        //registering user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (success, message,user) = await _authService.RegisterUserAsync(model);

            if (!success)
                return BadRequest(new { success = false, message });
            
            await _userManager.AddToRoleAsync(user, "Rider");

            return Ok(new { success = true, message });
        }

        // so path is api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _authService.LoginUserAsync(model);
            if (user == null)
                return Unauthorized(new { success = false, message = "Invalid credentials" });

            var tokens = _jwtGenerator.GenerateTokens(user, HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown");

            // TODO: (optional) Save the refresh token to DB if you want persistence
            // await _authService.SaveRefreshTokenAsync(user.Id, tokens.RefreshToken.Token, tokens.RefreshToken.Expires);

            return Ok(new
            {
                success = true,
                message = "Login successful",
                data = new
                {
                    
                    accessToken = tokens.AccessToken,
                    refreshToken = tokens.RefreshToken.Token
                }
            });
        }


        // get list of users api/auth/users
        // [Authorize]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(new
            {
                success = true,
                message = "All users retrived successfully",
                data=new
                {
                    numberOfUsers = users.Count,
                    users=users
                }
            });
        }
    }
}