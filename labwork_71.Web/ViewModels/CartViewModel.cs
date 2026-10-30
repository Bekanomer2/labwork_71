namespace labwork_71.ViewModels;

public class CartViewModel
{
    public int CafeId { get; set; }
    public List<CartItemViewModel> Items { get; set; } = new();
    public double TotalPrice => Items.Sum(item => item.Price * item.Quantity);
}