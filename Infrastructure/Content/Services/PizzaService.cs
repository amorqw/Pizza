using System.Data.Common;
using Core.Dto.Pizza;
using Dapper;
using Core.Interfaces;
using Core.Models;
using Npgsql;

namespace Infrastructure.Content.Services;

public class PizzaService : IPizzas
{
    public async Task<IEnumerable<Pizzas>> GetAllPizzasAsync()
    {
        const string query = @"
                SELECT 
                    PizzaId, 
                    Title, 
                    Description, 
                    Price, 
                    Size, 
                    Receipt
                FROM Pizzas";

        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<Pizzas>(query);
        }
    }

    public async Task<Pizzas> GetPizza(int id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Pizzas>(@"SELECT * 
                                                                    FROM pizzas 
                                                                    WHERE PizzaId = @id",
                       new { id })
                   ?? new Pizzas();
        }
    }


    public async Task<Pizzas> CreatePizza(PizzaDto pizza)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();

            string sql = @"insert into Pizzas (Title, Description, Price, Size, Receipt) 
                       values (@Title, @Description, @Price, @Size, @Receipt) 
                       returning PizzaId";  

            var newPizzaId = await connection.QueryFirstOrDefaultAsync<int>(sql, pizza);

            if (newPizzaId > 0)
            {
                
                string selectSql = @"select PizzaId, Title, Description, Price, Size, Receipt 
                                 from Pizzas 
                                 where PizzaId = @PizzaId";

                return await connection.QueryFirstOrDefaultAsync<Pizzas>(selectSql, new { Id = newPizzaId });
            }

            return null;
        }
    }


    public async Task<Pizzas> UpdatePizza(int pizzaId, PizzaDto pizza)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            string sql = @"
            UPDATE Pizzas 
            SET Title = @Title, Description = @Description, Price = @Price, Size = @Size, Receipt = @Receipt
            WHERE PizzaId = @PizzaId
            RETURNING *";

            return await connection.QueryFirstOrDefaultAsync<Pizzas>(sql, new
            {
                PizzaId = pizzaId,
                pizza.Title,
                pizza.Description,
                pizza.Price,
                pizza.Size,
                pizza.Receipt
            });
        }
    }


    public async Task<bool> DeletePizza(int Pizzaid)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            var pizza = await connection.QueryFirstOrDefaultAsync<Pizzas>(
                "select * from pizzas where PizzaId = @PizzaId", new { PizzaId = Pizzaid });
            if (pizza == null)
                return false;
            string sql = @"delete from Pizzas where PizzaId = @PizzaId";
            return await connection.ExecuteAsync(sql, new { PizzaId = Pizzaid }) > 0;
        }
    }
    public async Task<IEnumerable<Pizzas>> GetAllPizzasAsync(string orderBy = "Price", string sortDirection = "ASC", string size = null)
    {
        // Сортировка по цене или названию и направление сортировки
        var sortByColumn = orderBy == "Name" ? "Title" : "Price";
        var orderDirection = sortDirection.ToUpper() == "DESC" ? "DESC" : "ASC";

        // Фильтрация по размеру пиццы
        var sizeFilter = string.IsNullOrEmpty(size) ? "" : "WHERE Size = @Size";
    
        const string queryTemplate = @"
        SELECT 
            PizzaId, 
            Title, 
            Description, 
            Price, 
            Size, 
            Receipt
        FROM Pizzas
        {0}
        ORDER BY {1} {2}";

        var query = string.Format(queryTemplate, sizeFilter, sortByColumn, orderDirection);

        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<Pizzas>(query, new { Size = size });
        }
    }

    
}