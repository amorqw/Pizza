
using Dapper;
using Core.Interfaces;
using Core.Models;
using Npgsql;

namespace Infrastructure.Content.Services;

public class PizzeriaService : IPizzeria
{
    public async Task<IEnumerable<Pizzeria>> GetAllPizzerias()
    {
        const string query = @"
                SELECT *
                FROM Pizzerias";

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

            string sql = @"insert into Pizzerias (Title, Rating, Address, Courieramount) 
                       values (@Title, @Rating, @Address, @Courieramount) 
                       returning PizzeriaId";  

            var newPizzeriaId = await connection.QueryFirstOrDefaultAsync<int>(sql, pizzeria);

            if (newPizzeriaId > 0)
            {
                
                string selectSql = @"select PizzeriaId, Title, Rating, Address, Courieramount
                                 from Pizzerias
                                 where PizzeriaId = @PizzeriaId";

                return await connection.QueryFirstOrDefaultAsync<Pizzeria>(selectSql, new { Id = newPizzeriaId });
            }

            return null;
        }
    }


    public async Task<Pizzeria> UpdatePizzeria(int pizzeriaId, Pizzeria pizzeria)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            string sql = @"
            UPDATE Pizzerias
            SET Title = @Title, Rating = @Rating, Address = @Address, Courieramount = @Courieramount
            WHERE PizzeriaId = @PizzeriaId
            RETURNING *";
            return await connection.QueryFirstOrDefaultAsync<Pizzeria>(sql, new
             {
                 PizzeriaId = pizzeriaId,
                 pizzeria.Title,
                 pizzeria.Address,
                 pizzeria.Rating,
                 pizzeria.CourierAmount
             });
         }
    }


        public async Task<bool> DeletePizzeria(int pizzeriaId)
    {
        using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
        {
            connection.Open();
            var pizza = await connection.QueryFirstOrDefaultAsync<Pizzeria>(
                "select * from pizzerias where PizzeriaId = @PizzeriaId", new { PizzeriaId = pizzeriaId });
            if (pizza == null)
                return false;
            string sql = @"delete from Pizzerias where PizzeriaId = @PizzeriaId";
            return await connection.ExecuteAsync(sql, new { PizzeriaId = pizzeriaId }) > 0;
        }
    }
}