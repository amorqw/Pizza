using System.Data.Common;
using Core.Dto.Pizza;
using Dapper;
using Core.Interfaces;
using Core.Models;
using Npgsql;

namespace Infrastructure.Content.Services;

public class PizzaService : IPizzas
{
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
            connection.Open();
            string sql = @"insert into Pizzas (NamePizza,Description,Price,Size,Available) 
values (@NamePizza,@Description,@Price,@Size,@Available)";
            return await connection.QueryFirstOrDefaultAsync<Pizzas>(sql, pizza);
        }
    }

    public async Task<Pizzas> UpdatePizza(int pizzaId, PizzaDto pizza)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            string sql = @"
            UPDATE Pizzas 
            SET NamePizza = @NamePizza, Description = @Description, Price = @Price, Size = @Size, Available = @Available
            WHERE PizzaId = @PizzaId
            RETURNING *";
        
            return await connection.QueryFirstOrDefaultAsync<Pizzas>(sql, new 
            {
                PizzaId = pizzaId,
                pizza.NamePizza,
                pizza.Description,
                pizza.Price,
                pizza.Size,
                pizza.Available
            });
        }
    }


    public async Task<bool> DeletePizza(int Pizzaid)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            var pizza= await connection.QueryFirstOrDefaultAsync<Pizzas>( "select * from pizzas where PizzaId = @PizzaId", new { PizzaId = Pizzaid });
            if (pizza == null)
                return false;
            string sql =@"delete from Pizzas where PizzaId = @PizzaId";
            return await connection.ExecuteAsync(sql, new { PizzaId = Pizzaid }) > 0;
        }
    }
    
}