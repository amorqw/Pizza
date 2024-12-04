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
    public async Task<Users> GetUser(int id )
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Users>(@"select * from users where id = @id", new { id });
        }
    }

    public async Task<Users> UpdateUser(UpdateUserDto userDto,int id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            string sql =
                @"update users set FirstName = @FirstName, LastName = @LastName , Email = @Email, PhoneNumber=@PhoneNumber where UserId= @id return *";
            return await connection.QueryFirstOrDefaultAsync<Users>(sql,userDto);
        }
    }

    public async Task<bool> DeleteUser(int id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            string sql = @"delete from users where id = @id";
            return await connection.ExecuteAsync(sql, new { id }) > 0;
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
                INSERT INTO users (FirstName, LastName, Email, Role, Password, PhoneNumber)
                VALUES (@FirstName, @LastName, @Email, @Role, @Password, @PhoneNumber)";
            return await connection.ExecuteAsync(sql, user);
        }
    }

    
}