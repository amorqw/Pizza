using Core.Interfaces;
using Core.Models;
using Core.Dto.Ingredients;
using Dapper;
using Npgsql;

namespace Infrastructure.Content.Services;

public class IngredientsService : IIngredients
{
    public async Task<Ingredients?> GetIngredientById(int ingredientId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<Ingredients>(
                @"SELECT * FROM Ingredients WHERE IngredientId = @IngredientId", 
                new { IngredientId = ingredientId });
        }
    }

    public async Task<IEnumerable<Ingredients>> GetAllIngredients()
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<Ingredients>("SELECT * FROM Ingredients");
        }
    }

    public async Task<bool> CreateIngredient(CreateIngrDto ingredient)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"INSERT INTO Ingredients (NameIngredient) 
                           VALUES (@NameIngredient)";
            var result = await connection.ExecuteAsync(sql, ingredient);
            return result > 0;
        }
    }

    public async Task<bool> UpdateIngredient(Ingredients ingredient)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"UPDATE Ingredients 
                           SET NameIngredient = @NameIngredient 
                           WHERE IngredientId = @IngredientId";
            var result = await connection.ExecuteAsync(sql, ingredient);
            return result > 0;
        }
    }

    public async Task<bool> DeleteIngredient(int ingredientId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"DELETE FROM Ingredients WHERE IngredientId = @IngredientId";
            var result = await connection.ExecuteAsync(sql, new { IngredientId = ingredientId });
            return result > 0;
        }
    }
}
