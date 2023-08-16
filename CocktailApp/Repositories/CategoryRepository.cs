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

    public async Task<ErrorOr<Category>> GetById(params Guid[] keys)
    {
        var category = await _context.Categories.FindAsync(keys);
        if (category is null)
            return Error.NotFound();

        return category;
    }

    public async Task<ErrorOr<IEnumerable<Category>>> GetAll()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<ErrorOr<Category>> Update(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<ErrorOr<Deleted>> Delete(params Guid[] keys)
    {
        _context.Categories.Remove(Category.CreateId(keys[0]));
        await _context.SaveChangesAsync();
        return Result.Deleted;
    }

    public async Task<ErrorOr<int>> Count()
    {
        return await _context.Categories.CountAsync();
    }
}