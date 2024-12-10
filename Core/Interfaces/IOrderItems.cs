using Core.Models;

namespace Core.Interfaces;

public interface IOrderItems
{
    Task<OrderItems?> GetOrderItemById(int orderItemId);
    Task<IEnumerable<OrderItems>> GetOrderItemsByOrderId(int orderId);
    Task<bool> AddOrderItem(OrderItems orderItem);
    Task<bool> UpdateOrderItem(OrderItems orderItem);
    Task<bool> DeleteOrderItem(int orderItemId);
}