using System.ComponentModel.DataAnnotations.Schema;

namespace labwork_71.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }

    [ForeignKey(nameof(Dish))]
    public int ProductId { get; set; }
    public Dish? Dish { get; set; }

    public int Quantity { get; set; }
}
