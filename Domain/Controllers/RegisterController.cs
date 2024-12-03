using Core.Dto;
using Core.Interfaces.Auth;
using Core.Mapper;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IAuth _auth;

        public RegisterController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = AuthMapper.MapRegDtoToModel(userDto);  
            await _auth.CreateUser(user); 

            return Ok(new { Message = "User successfully registered!" });
        }

       
    }
}