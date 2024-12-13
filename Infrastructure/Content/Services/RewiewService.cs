using Core.Dto.Review;
using Core.Interfaces;
using Core.Models;
using Dapper;
using Npgsql;

namespace Infrastructure.Content.Services;

public class ReviewsService : IReviews
{

    public async Task<IEnumerable<Reviews>> GetAllReviews()
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<Reviews>("select * from reviews");
        }
    }
    public async Task<Reviews?> GetReviewById(int reviewId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<Reviews>(
                @"SELECT * FROM Reviews WHERE ReviewId = @ReviewId",
                new { ReviewId = reviewId });
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

    public async Task<bool> AddReview(Reviews review)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"INSERT INTO Reviews (PizzaId, UserId, Rating, Comment, ReviewDate)
                           VALUES (@PizzaId, @UserId, @Rating, @Comment, @ReviewDate)";
            var result = await connection.ExecuteAsync(sql, review);
            return result > 0;
        }
    }

    public async Task<bool> UpdateReview(Reviews review)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"UPDATE Reviews
                           SET PizzaId = @PizzaId,
                               UserId = @UserId,
                               Rating = @Rating,
                               Comment = @Comment,
                               ReviewDate = @ReviewDate
                           WHERE ReviewId = @ReviewId";
            var result = await connection.ExecuteAsync(sql, review);
            return result > 0;
        }
    }

    public async Task<bool> DeleteReview(int reviewId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = "DELETE FROM Reviews WHERE ReviewId = @ReviewId";
            var result = await connection.ExecuteAsync(sql, new { ReviewId = reviewId });
            return result > 0;
        }
    }
}