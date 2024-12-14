using System.Data.Common;
using Core.Dto.Pizza;
using Dapper;
using Core.Interfaces;
using Core.Models;
using Npgsql;

namespace Infrastructure.Content.Services;

public class PizzeriaService  //IPizzeria
{
    public async Task<IEnumerable<Pizzeria>> GetAllPizzerias()
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
            return await connection.QueryAsync<Pizzeria>(query);
        }
    }

    public async Task<Pizzeria> GetPizzeria(int id)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Pizzeria>(@"SELECT * 
                                                                    FROM Pizzerias
                                                                    WHERE PizzeriaId = @id",
                       new { id })
                   ?? new Pizzeria();
        }
    }


    public async Task<Pizzeria> CreatePizzeria(Pizzeria pizzeria)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            await connection.OpenAsync();

            string sql = @"insert into Pizzas (Title, Description, Price, Size, Receipt) 
                       values (@Title, @Description, @Price, @Size, @Receipt) 
                       returning PizzaId";  

            var newPizzaId = await connection.QueryFirstOrDefaultAsync<int>(sql, pizzeria);

            if (newPizzaId > 0)
            {
                
                string selectSql = @"select PizzaId, Title, Description, Price, Size, Receipt 
                                 from Pizzas 
                                 where PizzaId = @PizzaId";

                return await connection.QueryFirstOrDefaultAsync<Pizzeria>(selectSql, new { Id = newPizzaId });
            }

            return null;
        }
    }


    /* public async Task<Pizzeria> UpdatePizzeria(int pizzaId, Pizzeria pizzeria)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            string sql = @"
            UPDATE Pizzas
            SET Title = @Title, Description = @Description, Price = @Price, Size = @Size, Receipt = @Receipt
            WHERE PizzaId = @PizzaId
            RETURNING *";
            return await connection.QueryFirstOrDefaultAsync<Pizzeria>(sql, pizzeria); // ПОТОМ УБРАТЬ ЭТУ СТРОКУ

            return await connection.QueryFirstOrDefaultAsync<Pizzas>(sql, new
             {
                 PizzaId = pizzaId,
                 pizzeria.Title,
                 pizzeria.Description,
                 pizzeria.Price,
                 pizzeria.Size,
                 pizzeria.Receipt
             });
         }
    }*/


        /*public async Task<bool> DeletePizzeria(int pizzeriaId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            var pizza = await connection.QueryFirstOrDefaultAsync<Pizzas>(
                "select * from pizzas where PizzaId = @PizzaId", new { PizzaId = pizzeriaId });
            if (pizza == null)
                return false;
            string sql = @"delete from Pizzas where PizzaId = @PizzaId";
            return await connection.ExecuteAsync(sql, new { PizzaId = pizzeriaId }) > 0;
        }
    }*/
}