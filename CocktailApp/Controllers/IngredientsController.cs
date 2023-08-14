using CocktailApp.Contracts.Cocktail;
using CocktailApp.Contracts.Ingredient;
using CocktailApp.Models;
using CocktailApp.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CocktailApp.Controllers;

public class IngredientsController : ApiController
{
    private readonly IIngredientService _ingredientService;

    public IngredientsController(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    /// <summary>
    /// Creates an ingredient
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="201">Returns the newly created ingredient</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPost]
    public async Task<IActionResult> CreateIngredient(CreateIngredientRequest request)
    {
        var requestToIngredientResult = Ingredient.From(request);

        if (requestToIngredientResult.IsError)
            return Problem(requestToIngredientResult.Errors);
        var ingredient = requestToIngredientResult.Value;

        var createIngredientResult = await _ingredientService.CreateIngredient(ingredient);
        return createIngredientResult.Match(
            _ => CreatedAtAction(
                nameof(CreateIngredient),
                new { id = ingredient.Id },
                MapIngredientResponse(ingredient)
            ), Problem);
    }

    /// <summary>
    /// Gets all ingredients
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetIngredients()
    {
        var getIngredientsResult = await _ingredientService.GetIngredients();
        return getIngredientsResult.Match(
            ingredients => Ok(ingredients.Select(MapIngredientResponse)),
            Problem
        );
    }

    /// <summary>
    /// Gets an ingredient by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Returns the ingredient</response>
    /// <response code="404">If the ingredient is not found</response>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetIngredient(Guid id)
    {
        var getIngredientResult = await _ingredientService.GetIngredient(id);

        return getIngredientResult.Match(
            ingredient => Ok(MapIngredientResponse(ingredient)),
            Problem
        );
    }

    /// <summary>
    /// Updates an ingredient by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="204">If the ingredient is updated</response>
    /// <response code="400">If the request is invalid</response>
    /// <response code="404">If the ingredient is not found</response>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateIngredient(Guid id, UpdateIngredientRequest request)
    {
        var requestToIngredientResult = Ingredient.From(id, request);

        if (requestToIngredientResult.IsError)
            return Problem(requestToIngredientResult.Errors);
        var ingredient = requestToIngredientResult.Value;

        var updateIngredientResult = await _ingredientService.UpdateIngredient(ingredient);
        return updateIngredientResult.Match(_ => NoContent(), Problem);
    }

    /// <summary>
    /// Deletes an ingredient by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteIngredient(Guid id)
    {
        var deleteIngredientResult = await _ingredientService.DeleteIngredient(id);
        return deleteIngredientResult.Match(_ => NoContent(), Problem);
    }

    private static IngredientResponse MapIngredientResponse(Ingredient ingredient)
    {
        var response = new IngredientResponse(
            ingredient.Id,
            ingredient.Name,
            ingredient.Description,
            ingredient.CategoryId,
            ingredient.Abv
        );
        return response;
    }
}