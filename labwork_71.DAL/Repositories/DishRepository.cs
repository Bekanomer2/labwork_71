using labwork_71.DAL.Data;
using labwork_71.DAL.Repositories.Interfaces;
using labwork_71.Models;
using Microsoft.EntityFrameworkCore;

namespace labwork_71.DAL.Repositories;

public class DishRepository : IDishRepository
{
    private readonly FoodOrdersContext _context;

    public DishRepository(FoodOrdersContext context)
    {
        _context = context;
    }

    public async Task<List<Dish>> GetAllAsync()
    {
        return await _context.Dishes.Include(dish => dish.Cafe).ToListAsync();
    }

    public async Task<Dish?> GetByIdAsync(int id)
    {
        return await _context.Dishes.Include(dish => dish.Cafe).FirstOrDefaultAsync(dish => dish.Id == id);
    }

    public async Task AddAsync(Dish dish)
    {
        await _context.Dishes.AddAsync(dish);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Dish dish)
    {
        _context.Dishes.Remove(dish);
        await _context.SaveChangesAsync();
    }
}