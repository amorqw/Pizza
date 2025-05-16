using Core.Models;

namespace Core.Interfaces.Auth;

public interface IAuth
{
    Task<int> Register(string firstName, string lastName, string? middleName, string email, string password, string phoneNumber, Guid idRole);
    Task<string> Login(string email, string password);
    Task<int> CreateUser(Users user);
}