using System.Data;
using Core.Dto.RepairOrder;
using Core.Interfaces;
using Core.Models;
using Dapper;
using Npgsql;

namespace Infrastructure.Content.Services;

public class RepairOrderService : IRepairOrderService
{
    public async Task<RepairOrder> CreateRepairOrder(RepairOrderDto dto, Guid userId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"
                INSERT INTO repairorder (id_order, id_service, id_user, car_model, car_registration_number, problem_description, datetime, status)
                VALUES (@IdOrder, @ServiceId, @UserId, @CarModel, @CarRegistrationNumber, @ProblemDescription, @DateTime, @Status)
                RETURNING *";
            
            return await connection.QueryFirstOrDefaultAsync<RepairOrder>(sql, new
            {
                IdOrder = Guid.NewGuid(),
                ServiceId = dto.ServiceId,
                UserId = userId,
                CarModel = dto.CarModel,
                CarRegistrationNumber = dto.CarRegistrationNumber,
                ProblemDescription = dto.ProblemDescription,
                DateTime = dto.DateTime,
                Status = "Pending"
            });
        }
    }

    public async Task<RepairOrder> CreateAdminRepairOrder(AdminCreateRepairOrderDto dto)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"
                INSERT INTO repairorder (id_order, id_service, id_user, car_model, car_registration_number, problem_description, datetime, status)
                VALUES (@IdOrder, @ServiceId, @UserId, @CarModel, @CarRegistrationNumber, @ProblemDescription, @DateTime, @Status)
                RETURNING *";
            
            return await connection.QueryFirstOrDefaultAsync<RepairOrder>(sql, new
            {
                IdOrder = Guid.NewGuid(),
                ServiceId = dto.ServiceId,
                UserId = dto.UserId,
                CarModel = dto.CarModel,
                CarRegistrationNumber = dto.CarRegistrationNumber,
                ProblemDescription = dto.ProblemDescription,
                DateTime = dto.DateTime,
                Status = dto.Status
            });
        }
    }

    public async Task<RepairOrder> UpdateAdminOrder(AdminCreateRepairOrderDto orderDto, Guid id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"
                UPDATE repairorder
                SET 
                    id_service = @ServiceId,
                    id_user = @UserId,
                    car_model = @CarModel,
                    car_registration_number = @CarRegistrationNumber,
                    problem_description = @ProblemDescription,
                    datetime = @DateTime,
                    status = @Status
                WHERE id_order = @Id
                RETURNING *";
            
            return await connection.QueryFirstOrDefaultAsync<RepairOrder>(sql, new
            {
                Id = id,
                ServiceId = orderDto.ServiceId,
                UserId = orderDto.UserId,
                CarModel = orderDto.CarModel,
                CarRegistrationNumber = orderDto.CarRegistrationNumber,
                ProblemDescription = orderDto.ProblemDescription,
                DateTime = orderDto.DateTime,
                Status = orderDto.Status
            });
        }
    }

    public async Task<IEnumerable<Service>> GetAllServices()
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<Service>(@"
                SELECT 
                    id_service AS ""IdService"",
                    service_name AS ""ServiceName"",
                    cost AS ""Cost""
                FROM service");
        }
    }

    public async Task<bool> IsSlotAvailable(DateTime dateTime)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"
                SELECT COUNT(*) 
                FROM repairorder 
                WHERE datetime = @DateTime";
            
            var count = await connection.ExecuteScalarAsync<int>(sql, new { DateTime = dateTime });
            return count == 0;
        }
    }

    public async Task<IEnumerable<RepairOrder>> GetAllOrders()
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<RepairOrder>(@"
                SELECT 
                    id_order AS ""IdOrder"",
                    id_service AS ""ServiceId"",
                    id_user AS ""UserId"",
                    car_model AS ""CarModel"",
                    car_registration_number AS ""CarRegistrationNumber"",
                    problem_description AS ""ProblemDescription"",
                    datetime AS ""DateTime"",
                    status AS ""Status""
                FROM repairorder");
        }
    }

    public async Task<RepairOrder> GetOrder(Guid id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<RepairOrder>(@"
                SELECT 
                    id_order AS ""IdOrder"",
                    id_service AS ""ServiceId"",
                    id_user AS ""UserId"",
                    car_model AS ""CarModel"",
                    car_registration_number AS ""CarRegistrationNumber"",
                    problem_description AS ""ProblemDescription"",
                    datetime AS ""DateTime"",
                    status AS ""Status""
                FROM repairorder 
                WHERE id_order = @Id", new { Id = id });
        }
    }

    public async Task<RepairOrder> UpdateOrder(RepairOrderDto orderDto, Guid id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"
                UPDATE repairorder
                SET 
                    id_service = @ServiceId,
                    car_model = @CarModel,
                    car_registration_number = @CarRegistrationNumber,
                    problem_description = @ProblemDescription,
                    datetime = @DateTime,
                    status = @Status
                WHERE id_order = @Id
                RETURNING *";
            
            return await connection.QueryFirstOrDefaultAsync<RepairOrder>(sql, new
            {
                Id = id,
                ServiceId = orderDto.ServiceId,
                CarModel = orderDto.CarModel,
                CarRegistrationNumber = orderDto.CarRegistrationNumber,
                ProblemDescription = orderDto.ProblemDescription,
                DateTime = orderDto.DateTime,
                Status = orderDto.Status
            });
        }
    }

    public async Task<bool> DeleteOrder(Guid id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"DELETE FROM repairorder WHERE id_order = @Id";
            var result = await connection.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
} 