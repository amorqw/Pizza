using Core.Dto.User;
using Core.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/[controller]")]
public class LoginController : Controller
{
    private readonly IAuth _authService; 

    public LoginController(IAuth authService)
    {
        _authService = authService;
    }
    [HttpGet]
    [Route("/login")]
    public IActionResult Index()
    {
        return View("Login", new LoginUserDto());
    }
    
    [HttpPost("/login")]
    public async Task<IActionResult> LoginUser([FromForm]LoginUserDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        try
        {
            var token = await _authService.Login(request.Email, request.Password);
            return RedirectToAction("Index", "Home");
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