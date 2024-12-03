using Core.Models;

namespace Core.Interfaces.Auth;

public interface IAuth
{
    Task<int> CreateUser(Users user);
}