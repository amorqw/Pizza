using Core.Dto;
using Core.Models;

namespace Core.Interfaces;

public interface IUser
{
    Task<IEnumerable<Users>> GetAllUsers();
    Task<Users> GetUser(int id);
    Task<Users> UpdateUser(UpdateUserDto userDto, int id);
    Task<bool> DeleteUser(int id);
    Task<int> CreateUser(Users users);
    Task<Users> GetUserByEmail(string email);
}