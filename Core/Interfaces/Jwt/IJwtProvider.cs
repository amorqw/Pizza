using Core.Models;

namespace Core.Interfaces.Jwt;

public interface IJwtProvider
{
    string GenerateToken(Users users);
}