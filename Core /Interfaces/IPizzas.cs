using Core.Dto.Pizza;
using Core.Models;
namespace Core.Interfaces;


public interface IPizzas
{
    Task<Pizzas> GetPizza(int id);
    Task<Pizzas> CreatePizza(PizzaDto pizza);
    Task<Pizzas> UpdatePizza(int id ,PizzaDto pizza);
    Task<bool> DeletePizza(int pizzaid);

} 