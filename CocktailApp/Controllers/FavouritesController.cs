using CocktailApp.Contracts.Favourite;
using CocktailApp.Models;
using CocktailApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CocktailApp.Controllers;

public class FavouritesController : ApiController
{
    private readonly FavouriteService<CocktailFavourite> _cocktailFavouriteService;
    private readonly FavouriteService<CocktailListFavourite> _listFavouriteService;

    public FavouritesController(FavouriteService<CocktailFavourite> cocktailFavouriteService,
        FavouriteService<CocktailListFavourite> listFavouriteService)
    {
        _cocktailFavouriteService = cocktailFavouriteService;
        _listFavouriteService = listFavouriteService;
    }

    [HttpGet("cocktails")]
    public Task<IActionResult> GetCurrentUserFavouriteCocktails()
    {
        throw new NotImplementedException();
    }

    [HttpPost("cocktails")]
    public Task<IActionResult> UpdateCurrentUserFavouriteCocktails(FavouriteRequest request)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("lists")]
    public Task<IActionResult> GetCurrentUserFavouriteLists()
    {
        throw new NotImplementedException();
    }

    [HttpPost("lists")]
    public Task<IActionResult> UpdateCurrentUserFavouriteLists(FavouriteRequest request)
    {
        throw new NotImplementedException();
    }
}