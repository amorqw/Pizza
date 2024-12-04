using System.ComponentModel.DataAnnotations;

namespace Core.Dto.User;

public class LoginUserDto
{
     public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}