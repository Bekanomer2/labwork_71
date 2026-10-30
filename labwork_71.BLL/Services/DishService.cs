using labwork_71.BLL.Services.Interfaces;
using labwork_71.DAL.Repositories.Interfaces;
using labwork_71.Models;

namespace labwork_71.BLL.Services;

public class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;

    public DishService(IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task<List<Dish>> GetAllAsync()
    {
        return await _dishRepository.GetAllAsync();
    }

    public async Task<Dish?> GetByIdAsync(int id)
    {
        return await _dishRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Dish dish)
    {
        await _dishRepository.AddAsync(dish);
    }

    public async Task DeleteAsync(int id)
    {
        Dish? dish = await _dishRepository.GetByIdAsync(id);

        if (dish != null)
        {
            await _dishRepository.DeleteAsync(dish);
        }
    }
}