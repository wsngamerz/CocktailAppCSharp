using CocktailApp.Contracts.Favourite;
using Microsoft.AspNetCore.Mvc;

namespace CocktailApp.Controllers.Admin;

[Route("/api/favourites")]
public class AdminFavouritesController : ApiController
{
    
    [HttpGet("{userId:guid}/cocktails")]
    public Task<IActionResult> GetUserFavouriteCocktails(Guid userId)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{userId:guid}/cocktails")]
    public Task<IActionResult> UpdateUserFavouriteCocktails(Guid userId, FavouriteRequest request)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{userId:guid}/lists")]
    public Task<IActionResult> GetUserFavouriteLists(Guid userId)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{userId:guid}/lists")]
    public Task<IActionResult> UpdateUserFavouriteLists(Guid userId, FavouriteRequest request)
    {
        throw new NotImplementedException();
    }
}