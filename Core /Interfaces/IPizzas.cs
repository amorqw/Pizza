using Core.Models;
namespace Core.Interfaces;


public interface IPizzas
{
    Task<Pizzas> GetPizza(int id);
    Task<Pizzas> AddPizza(Pizzas pizza);
    Task<Pizzas> UpdatePizza(Pizzas pizza);
    Task<bool> DeletePizza(int pizzaid);

} 