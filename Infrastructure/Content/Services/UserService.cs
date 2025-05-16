using System.Data;
using Core.Dto;
using Core.Dto.User;
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

    public async Task<Users> GetUser(Guid id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            return await connection.QuerySingleOrDefaultAsync<Users>(
                "SELECT * FROM Users WHERE id_user = @IdUser", 
                new { IdUser = id }) ?? new Users();
        }
    }

    public async Task<Users> UpdateUser(UpdateUserDto userDto, Guid id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"
            UPDATE Users
            SET  
                first_name = @FirstName,
                last_name = @LastName,
                middle_name = @MiddleName,
                email = @Email,
                phone = @Phone,
                id_role = @IdRole
            WHERE id_user = @IdUser 
            RETURNING *";
        
            return await connection.QueryFirstOrDefaultAsync<Users>(sql, new
            {
                IdUser = id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                MiddleName = userDto.MiddleName,
                Email = userDto.Email,
                Phone = userDto.Phone,
                IdRole = userDto.IdRole
            });
        }
    }
    
    public async Task<bool> DeleteUser(Guid id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = "DELETE FROM Users WHERE id_user = @IdUser";
            var result = await connection.ExecuteAsync(sql, new { IdUser = id });
            return result > 0;
        }
    }

    public async Task<Users> GetUserByEmail(string email)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Users>(
                @"SELECT 
                    id_user AS ""IdUser"",
                    first_name AS ""FirstName"",
                    last_name AS ""LastName"",
                    middle_name AS ""MiddleName"",
                    phone AS ""Phone"",
                    email AS ""Email"",
                    password AS ""Password"",
                    id_role AS ""IdRole""
                FROM users WHERE email = @Email", new { Email = email });
        }
    }

    public async Task<int> CreateUser(Users user)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            string sql = @"
                INSERT INTO users (id_user, first_name, last_name, middle_name, email, id_role, password, phone)
                VALUES (@IdUser, @FirstName, @LastName, @MiddleName, @Email, @IdRole, @Password, @Phone)";
            return await connection.ExecuteAsync(sql, user);
        }
    }
}