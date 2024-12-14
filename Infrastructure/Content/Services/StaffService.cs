using Core.Dto;
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
            connection.Open();
            return await connection.QuerySingleOrDefaultAsync<Staff>(
                "SELECT * FROM Couriers WHERE StaffId = @StaffId", 
                new { StaffId = id }) ?? new Staff();


        }
    }

    public async Task<IEnumerable<Staff>> GetAllStaff()
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<Staff>("SELECT * FROM Couriers");
        }
    }

    public async Task<bool> AddStaff(StaffDto staff)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"INSERT INTO Couriers (FirstName, LastName, HireDate) 
                           VALUES (@FirstName, @LastName, @HireDate)";
            var result = await connection.ExecuteAsync(sql, staff);
            return result > 0;
        }
    }

    public async Task<Staff> UpdateStaff(UpdateStaffDto staff, int id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"UPDATE Couriers
                       SET  
                           FirstName = @FirstName,
                           LastName = @LastName
                       WHERE StaffId = @StaffId returning *";
            var updatedStaff = await connection.QueryFirstOrDefaultAsync<Staff>(sql, new
            {
                StaffId = id,
                FirstName = staff.FirstName,
                LastName = staff.LastName
            });

            Console.WriteLine($"Updated Staff: {updatedStaff?.StaffId}, {updatedStaff?.FirstName}, {updatedStaff?.LastName}");
        
            return updatedStaff;
        }
    }


    public async Task<bool> DeleteStaff(int id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = "DELETE FROM Couriers WHERE StaffId = @StaffId";
            var result = await connection.ExecuteAsync(sql, new { StaffId = id });
            return result > 0;
        }
    }
}