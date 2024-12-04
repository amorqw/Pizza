using Core.Dto;
using Core.Interfaces.Auth;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Dto.User;

namespace Pizza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authService; // Внедряем IAuth вместо AuthUserService

        // Внедрение зависимости через конструктор
        public AuthController(IAuth authService)
        {
            _authService = authService;
        }

        // Метод для регистрации пользователя
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Возвращаем ошибки, если модель невалидна
            }
            

            try
            {
                // Регистрация нового пользователя
                await _authService.Register(request.LastName, request.Email, request.Password, request.PhoneNumber);
                return Ok(new { Message = "User successfully registered!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Details = ex.Message });
            }
        }


        // Метод для логина пользователя
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Возвращаем ошибки, если модель невалидна
            }

            try
            {
                // Логин пользователя и получение токена
                var token = await _authService.Login(request.Email, request.Password);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Details = ex.Message });
            }
        }
    }
}
