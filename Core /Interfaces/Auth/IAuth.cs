using Core.Models;

namespace Core.Interfaces.Auth;

public interface IAuth
{
    Task<int> CreateUser(Users user);
    Task<string> Login(string email, string password);
    Task<int> Register(string userName, string email, string password, string PhoneNumber);
}