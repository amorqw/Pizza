using Core.Dto.PizzasAvailable;
using Core.Interfaces;
using Core.Models;
using Dapper;
using Npgsql;

namespace Infrastructure.Content.Services;

public class PizzasAvailableService : IPizzasAvailable
{
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