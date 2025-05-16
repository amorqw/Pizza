using Core.Interfaces;
using Core.Interfaces.Auth;
using Core.Interfaces.Jwt;
using Core.Models;
using Microsoft.AspNetCore.Http;

namespace Pizza.Services.Auth
{
    public class AuthService : IAuth
    {
        private readonly IUser _user;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUser user, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IHttpContextAccessor httpContextAccessor)
        {
            _user = user;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _httpContextAccessor = httpContextAccessor;  
        }

        public async Task<int> Register(string firstName, string lastName, string? middleName, string email, string password, string phoneNumber, Guid idRole)
        {
            var hashedPassword = _passwordHasher.Generate(password);
            var newUser = new Users
            {
                IdUser = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                Email = email,
                Password = hashedPassword,
                Phone = phoneNumber,
                IdRole = idRole
            };

            return await _user.CreateUser(newUser); 
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _user.GetUserByEmail(email); 
            if (user == null || !_passwordHasher.Verify(password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }
            var token = _jwtProvider.GenerateToken(user);
            return token;
        }

        public async Task<int> CreateUser(Users user)
        {
            return await _user.CreateUser(user); 
        }
    }
}
