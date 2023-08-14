using CocktailApp.Data;
using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly CocktailAppContext _context;

    public CategoryRepository(CocktailAppContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> Create(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return Result.Created;
    }

    public async Task<ErrorOr<Category>> GetById(Guid id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category is null)
            return Error.NotFound();

        return category;
    }

    public async Task<ErrorOr<IEnumerable<Category>>> GetMany()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<ErrorOr<Updated>> Update(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return Result.Updated;
    }


    public async Task<ErrorOr<Deleted>> Delete(Guid id)
    {
        _context.Categories.Remove(Category.CreateId(id));
        await _context.SaveChangesAsync();
        return Result.Deleted;
    }

    public async Task<ErrorOr<int>> Count()
    {
        return await _context.Categories.CountAsync();
    }
}