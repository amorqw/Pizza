using Core.Dto.Review;
using Core.Interfaces;
using Core.Models;
using Dapper;
using Npgsql;

namespace Infrastructure.Content.Services;

public class RewiewService : IReviews
{
    public async Task<IEnumerable<Reviews>> GetAllReviews()
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<Reviews>("SELECT * FROM Reviews");
        }
    }

    public async Task<Reviews?> GetReview(int pizzaId, int userId, int orderId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<Reviews>(
                @"SELECT * FROM Reviews 
                  WHERE PizzaId = @PizzaId AND UserId = @UserId AND OrderId = @OrderId",
                new { PizzaId = pizzaId, UserId = userId, OrderId = orderId });
        }
    }

    public async Task<IEnumerable<Reviews>> GetReviewsByPizzaId(int pizzaId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<Reviews>(
                @"SELECT * FROM Reviews WHERE PizzaId = @PizzaId",
                new { PizzaId = pizzaId });
        }
    }

    public async Task<IEnumerable<Reviews>> GetReviewsByUserId(int userId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<Reviews>(
                @"SELECT * FROM Reviews WHERE UserId = @UserId",
                new { UserId = userId });
        }
    }

    public async Task<bool> AddReview(ReviewDto review)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"INSERT INTO Reviews (PizzaId, UserId, OrderId, Rating, Comment, ReviewDate)
                           VALUES (@PizzaId, @UserId, @OrderId, @Rating, @Comment, @ReviewDate)";
            var result = await connection.ExecuteAsync(sql, review);
            return result > 0;
        }
    }

    public async Task<bool> UpdateReview(ReviewDto review)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"UPDATE Reviews
                           SET Rating = @Rating,
                               Comment = @Comment,
                               ReviewDate = @ReviewDate
                           WHERE PizzaId = @PizzaId AND UserId = @UserId AND OrderId = @OrderId";
            var result = await connection.ExecuteAsync(sql, review);
            return result > 0;
        }
    }

    public async Task<bool> DeleteReview(int pizzaId, int userId, int orderId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"DELETE FROM Reviews 
                           WHERE PizzaId = @PizzaId AND UserId = @UserId AND OrderId = @OrderId";
            var result = await connection.ExecuteAsync(sql, new { PizzaId = pizzaId, UserId = userId, OrderId = orderId });
            return result > 0;
        }
    }
}
