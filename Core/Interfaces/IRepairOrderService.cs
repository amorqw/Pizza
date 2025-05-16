using Core.Dto.RepairOrder;
using Core.Models;

namespace Core.Interfaces;

public interface IRepairOrderService
{
    Task<bool> IsSlotAvailable(DateTime dateTime);
    Task<RepairOrder> CreateRepairOrder(RepairOrderDto dto, Guid userId);
    Task<RepairOrder> CreateAdminRepairOrder(AdminCreateRepairOrderDto dto);
    Task<IEnumerable<Service>> GetAllServices();
    Task<IEnumerable<RepairOrder>> GetAllOrders();
    Task<RepairOrder> GetOrder(Guid id);
    Task<RepairOrder> UpdateOrder(RepairOrderDto orderDto, Guid id);
    Task<RepairOrder> UpdateAdminOrder(AdminCreateRepairOrderDto orderDto, Guid id);
    Task<bool> DeleteOrder(Guid id);
} 