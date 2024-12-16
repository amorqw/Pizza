using Core.Dto.PizzasAvailable;
using Core.Interfaces;
using Core.Models;
using Dapper;
using Npgsql;

namespace Infrastructure.Content.Services;

public class PizzasAvailableService : IPizzasAvailable
{
    public async Task<IEnumerable<Pizzas>> GetPizzasByPizzeria(int pizzeriaId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
        
            // Запрос, который извлекает доступные пиццы для пиццерии
            const string query = @"
            SELECT p.PizzaId, p.Title, p.Description, p.Price, p.Size, p.Receipt
            FROM Pizzas p
            INNER JOIN PizzasAvailable pa ON p.PizzaId = pa.PizzaId
            WHERE pa.PizzeriaId = @PizzeriaId AND pa.Available = TRUE";
        
            var result = await connection.QueryAsync<Pizzas>(query, new { PizzeriaId = pizzeriaId });
            return result;
        }
    }
    public async Task<IEnumerable<PizzasAvailableDto>> GetAll()
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            const string query = @"
            SELECT pa.PizzeriaId, pi.Title AS PizzeriaName, pa.PizzaId, p.Title AS PizzaName, pa.Available
            FROM PizzasAvailable pa
            JOIN Pizzas p ON pa.PizzaId = p.PizzaId
            JOIN Pizzerias pi ON pa.PizzeriaId = pi.PizzeriaId";
        
            var result = await connection.QueryAsync<PizzasAvailableDto>(query);
            return result;
        }
    }

    public async Task<PizzasAvailable?> GetById(int pizzeriaId, int pizzaId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            const string query = "SELECT * FROM PizzasAvailable WHERE PizzeriaId = @PizzeriaId AND PizzaId = @PizzaId";
            return await connection.QuerySingleOrDefaultAsync<PizzasAvailable>(query, new { PizzeriaId = pizzeriaId, PizzaId = pizzaId });
        }
    }
    public async Task<bool> Add(PizzasAvailable pizzasAvailable)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            const string query = "INSERT INTO PizzasAvailable (PizzeriaId, PizzaId, Available) VALUES (@PizzeriaId, @PizzaId, @Available)";
            var result = await connection.ExecuteAsync(query, pizzasAvailable);
            return result > 0;
        }
    }

    public async Task<bool> Update(PizzasAvailable pizzasAvailable)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            const string query = "UPDATE PizzasAvailable SET Available = @Available WHERE PizzeriaId = @PizzeriaId AND PizzaId = @PizzaId";
            var result = await connection.ExecuteAsync(query, pizzasAvailable);
            return result > 0;
        }
    }
    public async Task<bool> Delete(int pizzeriaId, int pizzaId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            const string query = "DELETE FROM PizzasAvailable WHERE PizzeriaId = @PizzeriaId AND PizzaId = @PizzaId";
            var result = await connection.ExecuteAsync(query, new { PizzeriaId = pizzeriaId, PizzaId = pizzaId });
            return result > 0;
        }
    }
    
}