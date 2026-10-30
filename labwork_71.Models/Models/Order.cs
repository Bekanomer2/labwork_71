namespace labwork_71.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CafeId { get; set; }
    public string RecipientName { get; set; }
    public string RecipientPhone { get; set; }
    public string DeliveryAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }  = new List<OrderItem>();
}