using labwork_71.DAL.Data;
using labwork_71.DAL.Repositories.Interfaces;
using labwork_71.Models;
using Microsoft.EntityFrameworkCore;

namespace labwork_71.DAL.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly FoodOrdersContext _context;

    public OrderRepository(FoodOrdersContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetUserOrdersAsync(int userId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(i => i.Dish)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(i => i.Dish)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(i => i.Dish)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }
}
