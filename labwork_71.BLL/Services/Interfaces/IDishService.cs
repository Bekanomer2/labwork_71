using labwork_71.Models;

namespace labwork_71.BLL.Services.Interfaces;

public interface IDishService
{
    Task<Dish?> GetByIdAsync(int id);
}