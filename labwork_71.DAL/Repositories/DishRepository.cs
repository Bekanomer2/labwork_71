using labwork_71.DAL.Data;
using labwork_71.DAL.Repositories.Interfaces;
using labwork_71.Models;
using Microsoft.EntityFrameworkCore;

namespace labwork_71.DAL.Repositories;

public class DishRepository: IDishRepository
{
    private readonly FoodOrdersContext _context;
    
    public DishRepository(FoodOrdersContext context)
    {
        _context = context;
    }

    public async Task<Dish?> GetByIdAsync(int id)
    {
        return await  _context.Dishes.FirstOrDefaultAsync(d=>d.Id == id);
    }
}