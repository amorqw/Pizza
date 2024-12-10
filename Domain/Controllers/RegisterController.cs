using Core.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;
using Core.Dto.User;

namespace Pizza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private readonly IAuth _authService; 

        public RegisterController(IAuth authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index()
        {
            return View("Index", new RegisterUserDto());
        }

        [HttpPost("/register")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            

            try
            {
                await _authService.Register(request.LastName, request.Email, request.Password, request.PhoneNumber);
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Details = ex.Message });
            }
        }


        /*[HttpPost("/login")]
        public async Task<IActionResult> LoginUser(LoginUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            try
            {
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
        } */
    }
}
