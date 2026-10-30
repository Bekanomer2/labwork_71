using labwork_71.Models;

namespace labwork_71.DAL.Repositories.Interfaces;

public interface IOrderRepository
{
    Task <List<Order>> GetUserOrdersAsync(int userId);
    Task <List<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderAsync(int orderId);
    Task AddOrderAsync(Order order);
}