using Microsoft.AspNetCore.Identity;
using SafeBoda.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeBoda.Infrastructure
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //  Register for User
        public async Task<bool> RegisterUserAsync(RegisterDto model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            return result.Succeeded;
        }

        //  Login metod User
        public async Task<UserResponseDto?> LoginUserAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        //  Get list of users users
        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();

            var result = users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Email = u.Email,
                UserName = u.UserName,
                PhoneNumber = u.PhoneNumber,
                FirstName = u.FirstName,
                LastName = u.LastName
            }).ToList();

            return await Task.FromResult(result);
        }
    }
}
