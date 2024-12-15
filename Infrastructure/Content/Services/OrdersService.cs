using Core.Dto.Order;
using Core.Interfaces;
using Core.Models;
using Dapper;
using Npgsql;

namespace Infrastructure.Content.Services
{
    public class OrdersService : IOrders
    {
        public async Task<OrderDto?> GetOrderById(int orderId)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<OrderDto>(
                    @"SELECT * FROM Orders WHERE OrderId = @OrderId",
                    new { OrderId = orderId });
            }
        }

        public async Task<IEnumerable<Orders>> GetOrdersByUserId(int userId)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Orders>(
                    @"SELECT * FROM Orders WHERE UserId = @UserId",
                    new { UserId = userId });
            }
        }

        public async Task<IEnumerable<Orders>> GetAllOrders()
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Orders>("SELECT * FROM Orders");
            }
        }

        public async Task<bool> AddOrder(OrderDto order)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                await connection.OpenAsync();
                string sql = @"INSERT INTO Orders (UserId, StaffId, Date, Status, Address, PaymentMethod) 
                               VALUES (@UserId, @StaffId, @Date, @Status, @Address, @PaymentMethod)";
                var result = await connection.ExecuteAsync(sql, order);
                return result > 0;
            }
        }

        public async Task<bool> UpdateOrder(OrderDto order)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                await connection.OpenAsync();
                string sql = @"UPDATE Orders 
                               SET UserId = @UserId, 
                                   StaffId = @StaffId, 
                                   Date = @Date, 
                                   Status = @Status, 
                                   Address = @Address, 
                                   PaymentMethod = @PaymentMethod
                               WHERE OrderId = @OrderId";
                var result = await connection.ExecuteAsync(sql, order);
                return result > 0;
            }
        }

        public async Task<bool> DeleteOrder(int orderId)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                await connection.OpenAsync();
                string sql = @"DELETE FROM Orders WHERE OrderId = @OrderId";
                var result = await connection.ExecuteAsync(sql, new { OrderId = orderId });
                return result > 0;
            }
        }
    }
}
