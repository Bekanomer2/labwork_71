using labwork_71.DAL.Data;
using labwork_71.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace labwork_71.DAL.Repositories;

public class ValidatorRepository : IValidatorRepository
{
    private readonly FoodOrdersContext _context;

    public ValidatorRepository(FoodOrdersContext context)
    {
        _context = context;
    }
    public async Task<bool> CheckEmail(string email, int? id)
    {
        if(id.HasValue)
            return !await _context.Users.AnyAsync(u => u.Email == email &&  u.Id != id.Value);
        return !await _context.Users.AnyAsync(u => u.Email == email);
    }
    
}