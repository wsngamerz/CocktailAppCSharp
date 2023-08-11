using CocktailApp.Data;
using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Repositories;

public class UserRepository: IUserRepository
{
    private readonly CocktailAppContext _context;
    
    public UserRepository(CocktailAppContext context)
    {
        _context = context;    
    }
    
    public async Task<ErrorOr<Created>> Create(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return Result.Created;
    }

    public async Task<ErrorOr<User>> GetById(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
            return Error.NotFound();
        
        return user;
    }

    public async Task<ErrorOr<User>> GetByClerkId(string id)
    {
        var user = await _context.Users.SingleOrDefaultAsync(c => c.ClerkId == id);
        if (user is null)
            return Error.NotFound();
        
        return user;
    }

    public async Task<ErrorOr<IEnumerable<User>>> GetMany()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<ErrorOr<Updated>> Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        
        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> Delete(Guid id)
    {
        _context.Users.Remove(new User { Id = id });
        await _context.SaveChangesAsync();
        return Result.Deleted;
    }

    public async Task<ErrorOr<int>> Count()
    {
        return await _context.Users.CountAsync();
    }
}