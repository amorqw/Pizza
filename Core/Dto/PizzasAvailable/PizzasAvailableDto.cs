using Core.Models;

namespace Core.Dto.PizzasAvailable;

public class PizzasAvailableDto
{
    public int PizzeriaId { get; set; }
    public string PizzeriaName { get; set; }
    public int PizzaId { get; set; }
    public string PizzaName { get; set; }
    public bool Available { get; set; }
}

