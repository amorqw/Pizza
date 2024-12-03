using Core.Models;

namespace Core.Interfaces;

public interface IOrders
{
    Task<Orders?> GetOrderById(int orderId);
    Task<IEnumerable<Orders>> GetOrdersByUserId(int userId);
    Task<IEnumerable<Orders>> GetAllOrders();
    Task<bool> AddOrder(Orders order);
    Task<bool> UpdateOrder(Orders order);
    Task<bool> DeleteOrder(int orderId);
}