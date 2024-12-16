using System.Data.Common;
using Dapper;
using Core.Interfaces;
using Core.Models;
using Npgsql;

namespace Infrastructure.Content.Services
{
    public class OrderItemsService : IOrderItems
    {
        public async Task<IEnumerable<OrderItems>> GetAllOrderItemsAsync()
        {
            const string query = @"
                SELECT 
                    OrderId, 
                    PizzaId, 
                    Amount
                FROM OrderItems";

            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<OrderItems>(query);
            }
        }

        public async Task<OrderItems> GetOrderItem(int orderId, int pizzaId)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<OrderItems>(
                    @"SELECT * 
                      FROM OrderItems 
                      WHERE OrderId = @orderId AND PizzaId = @pizzaId",
                    new { orderId, pizzaId }
                );
            }
        }

        public async Task<OrderItems> CreateOrderItem(OrderItems orderItem)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                await connection.OpenAsync();
                string sql = @"
                    INSERT INTO OrderItems (OrderId, PizzaId, Amount)
                    VALUES (@OrderId, @PizzaId, @Amount)
                    RETURNING OrderId, PizzaId, Amount";

                var newOrderItem = await connection.QueryFirstOrDefaultAsync<OrderItems>(sql, orderItem);
                return newOrderItem;
            }
        }

        public async Task<OrderItems> UpdateOrderItem(int orderId, int pizzaId, OrderItems orderItem)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                connection.Open();
                string sql = @"
                    UPDATE OrderItems 
                    SET Amount = @Amount
                    WHERE OrderId = @OrderId AND PizzaId = @PizzaId
                    RETURNING *";

                return await connection.QueryFirstOrDefaultAsync<OrderItems>(sql, new
                {
                    orderId,
                    pizzaId,
                    orderItem.Amount
                });
            }
        }

        public async Task<bool> DeleteOrderItem(int orderId, int pizzaId)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                connection.Open();
                var orderItem = await connection.QueryFirstOrDefaultAsync<OrderItems>(
                    "SELECT * FROM OrderItems WHERE OrderId = @OrderId AND PizzaId = @PizzaId", 
                    new { orderId, pizzaId });

                if (orderItem == null)
                    return false;

                string sql = "DELETE FROM OrderItems WHERE OrderId = @OrderId AND PizzaId = @PizzaId";
                return await connection.ExecuteAsync(sql, new { orderId, pizzaId }) > 0;
            }
        }
    }
}
