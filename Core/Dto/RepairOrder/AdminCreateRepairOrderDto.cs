using System.ComponentModel.DataAnnotations;

namespace Core.Dto.RepairOrder;

public class AdminCreateRepairOrderDto
{
    public Guid IdOrder { get; set; }
    
    [Required(ErrorMessage = "Пожалуйста, введите ID пользователя")]
    public Guid UserId { get; set; }
    
    [Required(ErrorMessage = "Пожалуйста, выберите дату и время")]
    public DateTime DateTime { get; set; }
    
    [Required(ErrorMessage = "Пожалуйста, выберите услугу")]
    public Guid ServiceId { get; set; }
    
    [Required(ErrorMessage = "Пожалуйста, введите модель автомобиля")]
    public string CarModel { get; set; }
    
    [Required(ErrorMessage = "Пожалуйста, введите государственный номер")]
    [StringLength(9, MinimumLength = 8, ErrorMessage = "Гос. номер должен содержать от 8 до 9 символов")]
    [RegularExpression(@"^[АВЕКМНОРСТУХ]\d{3}[АВЕКМНОРСТУХ]{2}\d{2,3}$", 
        ErrorMessage = "Неверный формат гос. номера. Пример: А159АА159")]
    public string CarRegistrationNumber { get; set; }
    
    [Required(ErrorMessage = "Пожалуйста, выберите статус")]
    public string Status { get; set; }
    
    [Required(ErrorMessage = "Пожалуйста, опишите проблему")]
    public string ProblemDescription { get; set; }
} 