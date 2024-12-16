using Core.Dto.PizzasAvailable;
using Core.Models;

namespace Core.Interfaces;

public interface IPizzasAvailable
{
    Task<IEnumerable<PizzasAvailableDto>> GetAll();
    Task<PizzasAvailable?> GetById(int pizzeriaId, int pizzaId);
    Task<bool> Add(PizzasAvailable pizzasAvailable);
    Task<bool> Update(PizzasAvailable pizzasAvailable);
    Task<bool> Delete(int pizzeriaId, int pizzaId);
    
}