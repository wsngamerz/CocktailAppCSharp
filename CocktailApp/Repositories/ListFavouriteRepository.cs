using CocktailApp.Data;
using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Repositories;

public class CocktailListFavouriteRepository : ICocktailListFavouriteRepository
{
    private readonly CocktailAppContext _context;

    public CocktailListFavouriteRepository(CocktailAppContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> Create(CocktailListFavourite value)
    {
        await _context.CocktailListFavourites.AddAsync(value);
        await _context.SaveChangesAsync();
        return Result.Created;
    }

    public async Task<ErrorOr<CocktailListFavourite>> Get(params Guid[] ids)
    {
        if (ids.Length != 2)
            return Error.Validation();
        var userId = ids[0];
        var listId = ids[1];

        var listFavourite = await _context.CocktailListFavourites.FindAsync(userId, listId);
        if (listFavourite is null)
            return Error.NotFound();

        return listFavourite;
    }

    public async Task<ErrorOr<IEnumerable<CocktailListFavourite>>> All()
    {
        return await _context.CocktailListFavourites.ToListAsync();
    }

    public async Task<ErrorOr<CocktailListFavourite>> Update(CocktailListFavourite value)
    {
        _context.CocktailListFavourites.Update(value);
        await _context.SaveChangesAsync();

        return value;
    }

    public async Task<ErrorOr<Deleted>> Delete(params Guid[] ids)
    {
        if (ids.Length != 2)
            return Error.Validation();
        var userId = ids[0];
        var listId = ids[1];

        _context.CocktailListFavourites.Remove(CocktailListFavourite.CreateId(userId, listId));
        await _context.SaveChangesAsync();
        return Result.Deleted;
    }

    public async Task<ErrorOr<int>> Count()
    {
        return await _context.CocktailListFavourites.CountAsync();
    }

    public async Task<ErrorOr<IEnumerable<CocktailListFavourite>>> GetByUserId(Guid id)
    {
        var listFavourites = await _context.CocktailListFavourites.Where(item => item.UserId == id).ToListAsync();
        return listFavourites;
    }
}