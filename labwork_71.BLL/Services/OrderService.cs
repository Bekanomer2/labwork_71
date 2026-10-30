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
    public OrderService(IOrderRepository orderRepository, IEmailService emailService, UserManager<User> userManager)
    {
        _orderRepository = orderRepository;
        _emailService = emailService;
        _userManager = userManager;
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
        var itemLines = string.Join("\n", orderItems.Select(i => $"- Блюдо #{i.ProductId}: {i.Quantity} шт."));
        var body = $"Ваш заказ #{order.Id} принят!\n\nСостав:\n{itemLines}\n\nАдрес: {order.DeliveryAddress}";
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
