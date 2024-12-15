using Core.Models;

namespace Core.Interfaces;

public interface IPizzeria
{
    Task<IEnumerable<Pizzeria>> GetAllPizzerias();
    Task<Pizzeria> GetPizzeria(int id);
    Task<Pizzeria> CreatePizzeria(Pizzeria pizzeria);
    Task<Pizzeria> UpdatePizzeria(int pizzeriaId ,Pizzeria pizzeria);
    Task<bool> DeletePizzeria(int pizzeriaId);

}