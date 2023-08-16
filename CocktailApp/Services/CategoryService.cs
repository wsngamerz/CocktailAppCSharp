using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using CocktailApp.Services.Abstractions;
using ErrorOr;

namespace CocktailApp.Services;

public class CategoryService: ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<ErrorOr<Created>> CreateCategory(Category category)
    {
        return await _categoryRepository.Create(category);
    }

    public async Task<ErrorOr<Category>> GetCategory(Guid id)
    {
        return await _categoryRepository.GetById(id);
    }

    public async Task<ErrorOr<IEnumerable<Category>>> GetCategories()
    {   
        return await _categoryRepository.GetAll();
    }

    public async Task<ErrorOr<Category>> UpdateCategory(Category category)
    {
        return await _categoryRepository.Update(category);
    }

    public async Task<ErrorOr<Deleted>> DeleteCategory(Guid id)
    {
        return await _categoryRepository.Delete(id);
    }
}