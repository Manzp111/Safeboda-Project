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

        public AuthController(
            IAuthService authService,
            JwtGenerator jwtGenerator,
            IConfiguration configuration,
            UserManager<User> userManager)
        {
            _authService = authService;
            _jwtGenerator = jwtGenerator;
            _configuration = configuration;
            _userManager = userManager;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success, message, user) = await _authService.RegisterUserAsync(model);

            if (!success)
                return BadRequest(new { success = false, message });

            // Assign default role
            // await _userManager.AddToRoleAsync(user);

         
            return Ok(new
            {
                success = true,
                message = "User registered successfully",
               
                
            });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _authService.LoginUserAsync(model);
            if (user == null)
                return Unauthorized(new { success = false, message = "Invalid credentials" });

            var tokens = await _jwtGenerator.GenerateTokensAsync(
                new UserResponseDto
                {
                    Id = user.Id,
                    Email = user.Email
                },
                HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown"
            );

            // Optional: Save refresh token to DB if needed
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

        // GET: api/auth/users
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(new
            {
                success = true,
                message = "All users retrieved successfully",
                data = new
                {
                    numberOfUsers = users.Count,
                    users = users
                }
            });
        }
    }
}
