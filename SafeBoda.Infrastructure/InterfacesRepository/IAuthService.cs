using System.Collections.Generic;
using System.Threading.Tasks;
using SafeBoda.Core;

public interface IAuthService
{
    // Task<bool> RegisterUserAsync(RegisterDto model);
    // Task<(bool success, string message)> RegisterUserAsync(RegisterDto model);
    Task<(bool success, string message, User? user)> RegisterUserAsync(RegisterDto model);
    Task<UserResponseDto?> LoginUserAsync(LoginDto model);
    Task<List<UserResponseDto>> GetAllUsersAsync();
}