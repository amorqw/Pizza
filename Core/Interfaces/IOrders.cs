using Core.Dto.Order;
using Core.Models;

namespace Core.Interfaces;

public interface IOrders
{
    Task<OrderDto?> GetOrderById(int orderId);
    Task<IEnumerable<Orders>> GetOrdersByUserId(int userId);
    Task<IEnumerable<Orders>> GetAllOrders();
    Task<int?> AddOrderReturnId(OrderDto order);
    Task<bool> AddOrder(OrderDto order);
    Task<bool> UpdateOrder(OrderDto order);
    Task<bool> DeleteOrder(int orderId);
}