using labwork_71.Models;

namespace labwork_71.BLL.Services.Interfaces;

public interface IOrderService
{
    Task<Order?> GetOrderAsync(int orderId);
    Task AddOrderAsync(Order order, List<OrderItem> orderItems);
    Task <List<Order>> GetAllOrdersAsync();
    Task <List<Order>> GetUserOrdersAsync(int userId);
}