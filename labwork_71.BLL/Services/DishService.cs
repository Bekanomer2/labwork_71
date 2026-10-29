using labwork_71.BLL.Services.Interfaces;
using labwork_71.DAL.Repositories.Interfaces;
using labwork_71.Models;

namespace labwork_71.BLL.Services;

public class DishService:  IDishService
{
    private readonly IDishRepository _dishRepository;

    public DishService(IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task<Dish?> GetByIdAsync(int id)
    {
        return await _dishRepository.GetByIdAsync(id);
    }
}