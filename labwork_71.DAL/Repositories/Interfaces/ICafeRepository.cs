using labwork_71.Models;

namespace labwork_71.DAL.Repositories.Interfaces;

public interface ICafeRepository
{
    Task<List<Cafe>> GetAllAsync();
    Task<Cafe?> GetByIdAsync(int id);
    Task AddAsync(Cafe cafe);
    Task DeleteAsync(Cafe cafe);
}