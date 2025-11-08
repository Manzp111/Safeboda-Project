using Microsoft.AspNetCore.Mvc;

using SafeBoda.Authenticationkey;

namespace SafeBoda.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    
    
    {
        
        
        private readonly IAuthService _authService;
       
        private readonly JwtGenerator _jwtGenerator;

        public AuthController(IAuthService authService, JwtGenerator jwtGenerator)
        {
            _authService = authService;
            _jwtGenerator = jwtGenerator;
        }

        // post: api/auth/register
        //registering user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var (success, message) = await _authService.RegisterUserAsync(model);

            if (!success)
                return BadRequest(new { success = false, message });

            return Ok(new { success = true, message });
        }

        // so path is api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _authService.LoginUserAsync(model);
            if (user == null) return Unauthorized(new { success = false, message = "Invalid credentials" });

            var token = _jwtGenerator.GenerateJwtToken(user);

            return Ok(new
            {
                success = true,
                message = "Login successful",
                data = new { user, token }
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