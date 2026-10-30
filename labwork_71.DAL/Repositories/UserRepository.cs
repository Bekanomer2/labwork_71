using labwork_71.DAL.Data;
using labwork_71.DAL.Repositories.Interfaces;
using labwork_71.Models;
using Microsoft.EntityFrameworkCore;

namespace labwork_71.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FoodOrdersContext _context;

    public UserRepository(FoodOrdersContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
    }
}