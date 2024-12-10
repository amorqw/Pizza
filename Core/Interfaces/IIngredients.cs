using Core.Dto.Ingredients;
using Core.Models;

namespace Core.Interfaces;

public interface IIngredients
{
    Task<Ingredients?> GetIngredientById(int ingredientId); 
    Task<IEnumerable<Ingredients>> GetAllIngredients(); 
    Task<bool> CreateIngredient(CreateIngrDto ingredient); 
    Task<bool> UpdateIngredient(Ingredients ingredient); 
    Task<bool> DeleteIngredient(int ingredientId); 
}