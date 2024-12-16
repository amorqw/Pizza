using Core.Dto.Pizza;
using Core.Models;
namespace Core.Interfaces;


public interface IPizzas
{
    Task<IEnumerable<Pizzas>> GetAllPizzasAsync();
    Task<Pizzas> GetPizza(int id);
    Task<Pizzas> CreatePizza(PizzaDto pizza);
    Task<Pizzas> UpdatePizza(int id ,PizzaDto pizza);
    Task<bool> DeletePizza(int pizzaid);
    Task<IEnumerable<Pizzas>> GetAllPizzasAsync(string orderBy, string sortDirection, string size);

} 