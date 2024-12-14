using System.Data;
using Core.Dto;
using Core.Interfaces;
using Core.Interfaces.Auth;
using Core.Models;
using Dapper;
using Npgsql;

namespace Infrastructure.Content.Services;

public class UserService: IUser
{
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public async Task<IEnumerable<Users>> GetAllUsers()
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<Users>(@"select * from users");
        }
    }
    public async Task<Users> GetUser(int id )
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            return await connection.QuerySingleOrDefaultAsync<Users>(
                "SELECT * FROM Users WHERE UserId = @UserId", 
                new { UserId = id }) ?? new Users();
        }
    }

    public async Task<Users> UpdateUser(UpdateUserDto userDto, int id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"
            UPDATE Users
            SET  
                Name = @Name,
                SurName = @SurName,
                Email = @Email,
                Phone = @Phone
            WHERE UserId = @UserId 
            RETURNING *";
        
            return await connection.QueryFirstOrDefaultAsync<Users>(sql, new
            {
                UserId = id,
                Name = userDto.Name,
                SurName = userDto.SurName,
                Email = userDto.Email,
                Phone = userDto.Phone
            });
        }
    }


    public async Task<bool> DeleteUser(int id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = "DELETE FROM Users WHERE UserId = @userid";
            var result = await connection.ExecuteAsync(sql, new { UserId = id });
            return result > 0;
        }
    }

    public async Task<Users> GetUserByEmail(string email)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Users>(@"SELECT * FROM users WHERE email = @Email", new { Email = email });
        }
    }

    public async Task<int> CreateUser(Users user)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            string sql = @"
                INSERT INTO users (Name, SurName, Email, Role, Password, Phone)
                VALUES (@name, @Surname, @Email, @Role, @Password, @Phone)";
            return await connection.ExecuteAsync(sql, user);
        }
    }

    
}