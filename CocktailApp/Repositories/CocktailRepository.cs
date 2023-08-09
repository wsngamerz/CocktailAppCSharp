using CocktailApp.Data;
using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using CocktailApp.ServiceErrors;
using ErrorOr;

namespace CocktailApp.Repositories;

public class CocktailRepository: ICocktailRepository
{
    private readonly CocktailAppContext _context;
    
    public CocktailRepository(CocktailAppContext context)
    {
        _context = context;    
    }
    
    public ErrorOr<Created> Create(Cocktail cocktail)
    {
        _context.Cocktails.Add(cocktail);
        _context.SaveChanges();
        return Result.Created;
    }

    public ErrorOr<Cocktail> GetById(Guid id)
    {
        var cocktail = _context.Cocktails.Find(id);
        if (cocktail is null)
            return Errors.Cocktail.NotFound;
        
        return cocktail;
    }

    public ErrorOr<Cocktail> GetBySlug(string slug)
    {
        var cocktail = _context.Cocktails.SingleOrDefault(c => c.Slug == slug);
        if (cocktail is null)
            return Errors.Cocktail.NotFound;
        
        return cocktail;
    }

    public ErrorOr<IEnumerable<Cocktail>> GetMany()
    {
        return _context.Cocktails.ToList();
    }

    public ErrorOr<Updated> Update(Cocktail cocktail)
    {
        _context.Cocktails.Update(cocktail);
        _context.SaveChanges();
        
        return Result.Updated;
    }

    public ErrorOr<Deleted> Delete(Guid id)
    {
        _context.Cocktails.Remove(new Cocktail { Id = id });
        _context.SaveChanges();
        return Result.Deleted;
    }

    public ErrorOr<int> Count()
    {
        return _context.Cocktails.Count();
    }
}