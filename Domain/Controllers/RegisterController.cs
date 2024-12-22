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
                await _authService.Register(request.SurName, request.Email, request.Password, request.Phone);
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Details = ex.Message });
            }
        }
    }
}