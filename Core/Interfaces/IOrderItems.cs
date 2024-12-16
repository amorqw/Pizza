using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderItems
    {
        Task<IEnumerable<OrderItems>> GetAllOrderItemsAsync();
        Task<OrderItems> GetOrderItem(int orderId, int pizzaId);
        Task<OrderItems> CreateOrderItem(OrderItems orderItem);
        Task<OrderItems> UpdateOrderItem(int orderId, int pizzaId, OrderItems orderItem);
        Task<bool> DeleteOrderItem(int orderId, int pizzaId);
    }
}