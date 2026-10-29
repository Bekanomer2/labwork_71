using labwork_71.Models;

namespace labwork_71.BLL.Services.Interfaces;

public interface ICafeService
{
    Task<List<Cafe>> GetAllAsync();
    Task<Cafe?> GetByIdAsync(int id);
    Task AddAsync(Cafe cafe);
    Task DeleteAsync(int id);
}