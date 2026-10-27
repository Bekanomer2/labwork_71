namespace labwork_71.Models;

public class Cafe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AvatarUrl { get; set; }
    public string Description { get; set; }
    public List<Dish> Dishes { get; set; }
}