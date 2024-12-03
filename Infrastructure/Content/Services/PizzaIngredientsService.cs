using Core.Interfaces;
using Core.Models;
using Dapper;
using Npgsql;

namespace Infrastructure.Content.Services;

public class PizzaIngredientsService : IPizzaIngredients
{
    public async Task<IEnumerable<PizzaIngredients>> GetIngredientsByPizzaId(int pizzaId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<PizzaIngredients>(
                @"SELECT * FROM PizzaIngredients WHERE PizzaId = @PizzaId",
                new { PizzaId = pizzaId });
        }
    }

    public async Task<IEnumerable<PizzaIngredients>> GetPizzasByIngredientId(int ingredientId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<PizzaIngredients>(
                @"SELECT * FROM PizzaIngredients WHERE IngredientId = @IngredientId",
                new { IngredientId = ingredientId });
        }
    }

    public async Task<bool> AddPizzaIngredient(PizzaIngredients pizzaIngredient)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"INSERT INTO PizzaIngredients (PizzaId, IngredientId) 
                           VALUES (@PizzaId, @IngredientId)";
            var result = await connection.ExecuteAsync(sql, pizzaIngredient);
            return result > 0;
        }
    }

    public async Task<bool> DeletePizzaIngredient(int pizzaId, int ingredientId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            string sql = @"DELETE FROM PizzaIngredients 
                           WHERE PizzaId = @PizzaId AND IngredientId = @IngredientId";
            var result = await connection.ExecuteAsync(sql, new { PizzaId = pizzaId, IngredientId = ingredientId });
            return result > 0;
        }
    }
}