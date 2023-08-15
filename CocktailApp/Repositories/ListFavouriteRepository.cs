using CocktailApp.Data;
using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Repositories;

public class ListFavouriteRepository : IListFavouriteRepository
{
    private readonly CocktailAppContext _context;

    public ListFavouriteRepository(CocktailAppContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> Create(ListFavourite value)
    {
        await _context.ListFavourites.AddAsync(value);
        await _context.SaveChangesAsync();
        return Result.Created;
    }

    public async Task<ErrorOr<ListFavourite>> Get(params Guid[] ids)
    {
        if (ids.Length != 2)
            return Error.Validation();
        var userId = ids[0];
        var listId = ids[1];

        var listFavourite = await _context.ListFavourites.FindAsync(userId, listId);
        if (listFavourite is null)
            return Error.NotFound();

        return listFavourite;
    }

    public async Task<ErrorOr<IEnumerable<ListFavourite>>> All()
    {
        return await _context.ListFavourites.ToListAsync();
    }

    public async Task<ErrorOr<Updated>> Update(ListFavourite value)
    {
        _context.ListFavourites.Update(value);
        await _context.SaveChangesAsync();

        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> Delete(params Guid[] ids)
    {
        if (ids.Length != 2)
            return Error.Validation();
        var userId = ids[0];
        var listId = ids[1];

        _context.ListFavourites.Remove(ListFavourite.CreateId(userId, listId));
        await _context.SaveChangesAsync();
        return Result.Deleted;
    }

    public async Task<ErrorOr<int>> Count()
    {
        return await _context.ListFavourites.CountAsync();
    }

    public async Task<ErrorOr<IEnumerable<ListFavourite>>> GetByUserId(Guid id)
    {
        var listFavourites = await _context.ListFavourites.Where(item => item.UserId == id).ToListAsync();
        return listFavourites;
    }
}