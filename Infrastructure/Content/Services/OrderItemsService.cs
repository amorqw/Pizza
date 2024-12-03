using Core.Interfaces;
using Core.Models;
using Dapper;
using Npgsql;

namespace Infrastructure.Content.Services;

public class OrderItemsService : IOrderItems
{
    public async Task<OrderItems?> GetOrderItemById(int orderItemId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<OrderItems>(
                @"SELECT * FROM OrderItems WHERE OrderItemId = @OrderItemId",
                new { OrderItemId = orderItemId });
        }
    }

    public async Task<IEnumerable<OrderItems>> GetOrderItemsByOrderId(int orderId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<OrderItems>(
                @"SELECT * FROM OrderItems WHERE OrderId = @OrderId",
                new { OrderId = orderId });
        }
    }

    public async Task<bool> AddOrderItem(OrderItems orderItem)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"INSERT INTO OrderItems (OrderId, PizzaId, Quantity, ItemPrice) 
                           VALUES (@OrderId, @PizzaId, @Quantity, @ItemPrice)";
            var result = await connection.ExecuteAsync(sql, orderItem);
            return result > 0;
        }
    }

    public async Task<bool> UpdateOrderItem(OrderItems orderItem)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"UPDATE OrderItems 
                           SET OrderId = @OrderId, 
                               PizzaId = @PizzaId, 
                               Quantity = @Quantity, 
                               ItemPrice = @ItemPrice 
                           WHERE OrderItemId = @OrderItemId";
            var result = await connection.ExecuteAsync(sql, orderItem);
            return result > 0;
        }
    }

    public async Task<bool> DeleteOrderItem(int orderItemId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"DELETE FROM OrderItems WHERE OrderItemId = @OrderItemId";
            var result = await connection.ExecuteAsync(sql, new { OrderItemId = orderItemId });
            return result > 0;
        }
    }
}