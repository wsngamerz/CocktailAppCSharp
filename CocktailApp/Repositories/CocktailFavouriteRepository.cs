using CocktailApp.Data;
using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Repositories;

public class CocktailFavouriteRepository : ICocktailFavouriteRepository
{
    private readonly CocktailAppContext _context;

    public CocktailFavouriteRepository(CocktailAppContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> Create(CocktailFavourite value)
    {
        await _context.CocktailFavourites.AddAsync(value);
        await _context.SaveChangesAsync();
        return Result.Created;
    }

    public async Task<ErrorOr<CocktailFavourite>> Get(params Guid[] ids)
    {
        if (ids.Length != 2)
            return Error.Validation();
        var userId = ids[0];
        var cocktailId = ids[1];

        var cocktailFavourite = await _context.CocktailFavourites.FindAsync(userId, cocktailId);
        if (cocktailFavourite is null)
            return Error.NotFound();

        return cocktailFavourite;
    }

    public async Task<ErrorOr<IEnumerable<CocktailFavourite>>> All()
    {
        return await _context.CocktailFavourites.ToListAsync();
    }

    public async Task<ErrorOr<Updated>> Update(CocktailFavourite value)
    {
        _context.CocktailFavourites.Update(value);
        await _context.SaveChangesAsync();

        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> Delete(params Guid[] ids)
    {
        if (ids.Length != 2)
            return Error.Validation();
        var userId = ids[0];
        var cocktailId = ids[1];

        _context.CocktailFavourites.Remove(CocktailFavourite.CreateId(userId, cocktailId));
        await _context.SaveChangesAsync();
        return Result.Deleted;
    }

    public async Task<ErrorOr<int>> Count()
    {
        return await _context.CocktailFavourites.CountAsync();
    }

    public async Task<ErrorOr<IEnumerable<CocktailFavourite>>> GetByUserId(Guid id)
    {
        var cocktailFavourites = await _context.CocktailFavourites.Where(item => item.UserId == id).ToListAsync();
        return cocktailFavourites;
    }
}