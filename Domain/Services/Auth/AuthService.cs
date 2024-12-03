using Core.Interfaces.Auth;
using Core.Models;
using Infrastructure.Content.Services;

namespace Core.Services.Auth
{
    public class AuthService : IAuth
    {
        private readonly UserService _userService;

        public AuthService(UserService userService)
        {
            _userService = userService;
        }

        public async Task<int> CreateUser(Users user)
        {
            return await _userService.CreateUserr(user);
        }
    }
}