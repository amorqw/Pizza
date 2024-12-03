using Core.Interfaces;
using Core.Models;
using Dapper;
using Npgsql;

namespace Infrastructure.Content.Services;

public class StaffService : IStaff
{
    public async Task<Staff?> GetStaffById(int id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<Staff>(
                "SELECT * FROM Staff WHERE StaffId = @StaffId", 
                new { StaffId = id });
        }
    }

    public async Task<IEnumerable<Staff>> GetAllStaff()
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<Staff>("SELECT * FROM Staff");
        }
    }

    public async Task<bool> AddStaff(Staff staff)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"INSERT INTO Staff (FirstName, LastName, Position, HireDate) 
                           VALUES (@FirstName, @LastName, @Position, @HireDate)";
            var result = await connection.ExecuteAsync(sql, staff);
            return result > 0;
        }
    }

    public async Task<bool> UpdateStaff(Staff staff)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"UPDATE Staffs
                           SET FirstName = @FirstName, 
                               LastName = @LastName, 
                               Position = @Position, 
                               HireDate = @HireDate
                           WHERE StaffId = @StaffId";
            var result = await connection.ExecuteAsync(sql, staff);
            return result > 0;
        }
    }

    public async Task<bool> DeleteStaff(int id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = "DELETE FROM Staff WHERE StaffId = @StaffId";
            var result = await connection.ExecuteAsync(sql, new { StaffId = id });
            return result > 0;
        }
    }
}
