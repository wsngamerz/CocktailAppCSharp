using CocktailApp.Contracts.Category;
using CocktailApp.Models;
using CocktailApp.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CocktailApp.Controllers;

public class CategoriesController : ApiController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Creates a category
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="201">Returns the newly created category</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
    {
        var requestToCategoryResult = Category.From(request);

        if (requestToCategoryResult.IsError)
            return Problem(requestToCategoryResult.Errors);
        var category = requestToCategoryResult.Value;

        var createCategoryResult = await _categoryService.CreateCategory(category);
        return createCategoryResult.Match(
            _ => CreatedAtAction(
                nameof(CreateCategory),
                new { id = category.Id },
                MapCategoryResponse(category)
            ), Problem);
    }

    /// <summary>
    /// Gets all categories
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var getCategoriesResult = await _categoryService.GetCategories();
        return getCategoriesResult.Match(
            categories => Ok(categories.Select(MapCategoryResponse)),
            Problem
        );
    }

    /// <summary>
    /// Gets a category by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Returns the category</response>
    /// <response code="404">If the category is not found</response>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCategory(Guid id)
    {
        var getCategoryResult = await _categoryService.GetCategory(id);

        return getCategoryResult.Match(
            category => Ok(MapCategoryResponse(category)),
            Problem
        );
    }

    /// <summary>
    /// Updates a category by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="204">If the category is updated</response>
    /// <response code="400">If the request is invalid</response>
    /// <response code="404">If the category is not found</response>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryRequest request)
    {
        var requestToCategoryResult = Category.From(id, request);

        if (requestToCategoryResult.IsError)
            return Problem(requestToCategoryResult.Errors);
        var category = requestToCategoryResult.Value;

        var updateCategoryResult = await _categoryService.UpdateCategory(category);
        return updateCategoryResult.Match(_ => NoContent(), Problem);
    }

    /// <summary>
    /// Deletes a category by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var deleteCategoryResult = await _categoryService.DeleteCategory(id);
        return deleteCategoryResult.Match(_ => NoContent(), Problem);
    }

    private static CategoryResponse MapCategoryResponse(Category category)
    {
        var response = new CategoryResponse(
            category.Id,
            category.Name,
            category.Description,
            category.ParentId
        );
        return response;
    }
}