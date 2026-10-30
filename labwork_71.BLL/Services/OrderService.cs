using labwork_71.BLL.Services.Interfaces;
using labwork_71.DAL.Repositories.Interfaces;
using labwork_71.Models;
using Microsoft.AspNetCore.Identity;

namespace labwork_71.BLL.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IEmailService _emailService;
    private readonly UserManager<User> _userManager;
    private readonly IDishRepository _dishRepository;
    public OrderService(IOrderRepository orderRepository, IEmailService emailService, UserManager<User> userManager, IDishRepository dishRepository)
    {
        _orderRepository = orderRepository;
        _emailService = emailService;
        _userManager = userManager;
        _dishRepository = dishRepository;
    }

    public async Task<Order?> GetOrderAsync(int orderId)
    {
        return await _orderRepository.GetOrderAsync(orderId);
    }

    public async Task AddOrderAsync(Order order, List<OrderItem> orderItems)
    {
        order.OrderItems = orderItems;
        await _orderRepository.AddOrderAsync(order);
        var user = await _userManager.FindByIdAsync(order.UserId.ToString());
        string itemLines = "";
        foreach (OrderItem item in orderItems)
        {
            Dish? dish = await _dishRepository.GetByIdAsync(item.ProductId);
            if (dish != null)
            {
                itemLines += $"- {dish.Name}: {item.Quantity} шт.\n";
            }
        }
        var body = $"Ваш заказ #{order.Id} принят!\n\nСостав:\n{itemLines}\nАдрес: {order.DeliveryAddress}";
        await _emailService.SendMessageAsync(user.Email, "Заказ оформлен", body);
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllOrdersAsync();
    }

    public async Task<List<Order>> GetUserOrdersAsync(int userId)
    {
        return await _orderRepository.GetUserOrdersAsync(userId);
    }
}
