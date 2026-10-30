namespace labwork_71.ViewModels;

public class CartItemViewModel
{
    public int DishId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice => Price * Quantity;
}