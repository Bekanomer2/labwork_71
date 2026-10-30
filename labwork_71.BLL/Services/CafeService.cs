using labwork_71.BLL.Services.Interfaces;
using labwork_71.DAL.Repositories.Interfaces;
using labwork_71.Models;

namespace labwork_71.BLL.Services;

public class CafeService : ICafeService
{
    private readonly ICafeRepository _cafeRepository;

    public CafeService(ICafeRepository cafeRepository)
    {
        _cafeRepository = cafeRepository;
    }

    public async Task<List<Cafe>> GetAllAsync()
    {
        return await _cafeRepository.GetAllAsync();
    }

    public async Task<Cafe?> GetByIdAsync(int id)
    {
        return await _cafeRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Cafe cafe)
    {
        await _cafeRepository.AddAsync(cafe);
    }

    public async Task DeleteAsync(int id)
    {
        Cafe? cafe = await _cafeRepository.GetByIdAsync(id);

        if (cafe != null)
        {
            await _cafeRepository.DeleteAsync(cafe);
        }
    }
}