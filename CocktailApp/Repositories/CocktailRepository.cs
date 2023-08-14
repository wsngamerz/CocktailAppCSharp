using CocktailApp.Data;
using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Repositories;

public class CocktailRepository: ICocktailRepository
{
    private readonly CocktailAppContext _context;
    
    public CocktailRepository(CocktailAppContext context)
    {
        _context = context;    
    }
    
    public async Task<ErrorOr<Created>> Create(Cocktail cocktail)
    {
        await _context.Cocktails.AddAsync(cocktail);
        await _context.SaveChangesAsync();
        return Result.Created;
    }

    public async Task<ErrorOr<Cocktail>> GetById(Guid id)
    {
        var cocktail = await _context.Cocktails.FindAsync(id);
        if (cocktail is null)
            return Error.NotFound();
        
        return cocktail;
    }

    public async Task<ErrorOr<Cocktail>> GetBySlug(string slug)
    {
        var cocktail = await _context.Cocktails.SingleOrDefaultAsync(c => c.Slug == slug);
        if (cocktail is null)
            return Error.NotFound();
        
        return cocktail;
    }

    public async Task<ErrorOr<IEnumerable<Cocktail>>> GetMany()
    {
        return await _context.Cocktails.ToListAsync();
    }

    public async Task<ErrorOr<Updated>> Update(Cocktail cocktail)
    {
        _context.Cocktails.Update(cocktail);
        await _context.SaveChangesAsync();
        
        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> Delete(Guid id)
    {
        _context.Cocktails.Remove(Cocktail.CreateId(id));
        await _context.SaveChangesAsync();
        return Result.Deleted;
    }

    public async Task<ErrorOr<int>> Count()
    {
        return await _context.Cocktails.CountAsync();
    }
}