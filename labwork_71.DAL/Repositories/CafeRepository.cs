using labwork_71.DAL.Data;
using labwork_71.DAL.Repositories.Interfaces;
using labwork_71.Models;
using Microsoft.EntityFrameworkCore;

namespace labwork_71.DAL.Repositories;

public class CafeRepository : ICafeRepository
{
    private readonly FoodOrdersContext _context;

    public CafeRepository(FoodOrdersContext context)
    {
        _context = context;
    }

    public async Task<List<Cafe>> GetAllAsync()
    {
        return await _context.Cafes.ToListAsync();
    }

    public async Task<Cafe?> GetByIdAsync(int id)
    {
        return await _context.Cafes.Include(cafe => cafe.Dishes).FirstOrDefaultAsync(cafe => cafe.Id == id);
    }

    public async Task AddAsync(Cafe cafe)
    {
        await _context.Cafes.AddAsync(cafe);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Cafe cafe)
    {
        _context.Cafes.Remove(cafe);
        await _context.SaveChangesAsync();
    }
}