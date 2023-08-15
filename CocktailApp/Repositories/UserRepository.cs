using CocktailApp.Data;
using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Repositories;

public class UserRepository : IUserRepository
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

    public async Task<ErrorOr<User>> Get(params Guid[] ids)
    {
        var user = await _context.Users.FindAsync(ids[0]);
        if (user is null)
            return Error.NotFound();

        return user;
    }

    public async Task<ErrorOr<IEnumerable<User>>> All()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<ErrorOr<User>> GetByClerkId(string id)
    {
        var user = await _context.Users.SingleOrDefaultAsync(c => c.ClerkId == id);
        if (user is null)
            return Error.NotFound();

        return user;
    }

    public async Task<ErrorOr<Updated>> Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> Delete(params Guid[] ids)
    {
        _context.Users.Remove(User.CreateId(ids[0]));
        await _context.SaveChangesAsync();
        return Result.Deleted;
    }

    public async Task<ErrorOr<int>> Count()
    {
        return await _context.Users.CountAsync();
    }
}