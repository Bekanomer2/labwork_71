using labwork_71.Models;


namespace labwork_71.DAL.Repositories.Interfaces;

public interface IDishRepository
{
    Task<Dish?> GetByIdAsync(int id);
    Task<List<Dish>> GetAllAsync();
    Task AddAsync(Dish dish);
    Task DeleteAsync(Dish dish);
}