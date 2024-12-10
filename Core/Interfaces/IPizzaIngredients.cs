using Core.Models;

namespace Core.Interfaces;

public interface IPizzaIngredients
{
    Task<IEnumerable<PizzaIngredients>> GetIngredientsByPizzaId(int pizzaId);
    Task<IEnumerable<PizzaIngredients>> GetPizzasByIngredientId(int ingredientId);
    Task<bool> AddPizzaIngredient(PizzaIngredients pizzaIngredient);
    Task<bool> DeletePizzaIngredient(int pizzaId, int ingredientId);
}