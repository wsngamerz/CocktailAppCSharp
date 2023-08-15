using CocktailApp.Data;
using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Repositories;

public class BarItemRepository : IBarItemRepository
{
    private readonly CocktailAppContext _context;

    public BarItemRepository(CocktailAppContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> Create(BarItem barItem)
    {
        await _context.BarItems.AddAsync(barItem);
        await _context.SaveChangesAsync();
        return Result.Created;
    }

    public async Task<ErrorOr<BarItem>> Get(params Guid[] ids)
    {
        if (ids.Length != 2)
            return Error.Custom(0, "IncorrectArgCount", "Incorrect number of ids");
        var userId = ids[0];
        var ingredientId = ids[1];
        
        var barItem = await _context.BarItems.FindAsync(userId, ingredientId);
        if (barItem is null)
            return Error.NotFound();
        return barItem;
    }

    public async Task<ErrorOr<IEnumerable<BarItem>>> All()
    {
        return await _context.BarItems.ToListAsync();
    }

    public async Task<ErrorOr<IEnumerable<BarItem>>> GetByUserId(Guid userId)
    {
        var barItems = await _context.BarItems.Where(item => item.UserId == userId).ToListAsync();
        return barItems;
    }

    public async Task<ErrorOr<Updated>> Update(BarItem barItem)
    {
        _context.BarItems.Update(barItem);
        await _context.SaveChangesAsync();

        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> Delete(params Guid[] ids)
    {
        if (ids.Length != 2)
            return Error.Custom(0, "IncorrectArgCount", "Incorrect number of ids");
        var userId = ids[0];
        var ingredientId = ids[1];
        
        _context.BarItems.Remove(BarItem.CreateId(userId, ingredientId));
        await _context.SaveChangesAsync();
        return Result.Deleted;   
    }

    public async Task<ErrorOr<int>> Count()
    {
        return await _context.BarItems.CountAsync();
    }
}