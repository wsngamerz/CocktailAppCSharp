using CocktailApp.Contracts.User;
using CocktailApp.Models;
using CocktailApp.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CocktailApp.Controllers;

public class UsersController : ApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Gets all users
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var getUsersResult = await _userService.GetUsers();
        return getUsersResult.Match(
            users => Ok(users.Select(MapUserResponse)),
            Problem
        );
    }

    /// <summary>
    /// Gets a user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Returns the user</response>
    /// <response code="404">If the user is not found</response>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var getUserResult = await _userService.GetUser(id);

        return getUserResult.Match(
            user => Ok(MapUserResponse(user)),
            Problem
        );
    }

    /// <summary>
    /// Updates a user by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="204">If the user is updated</response>
    /// <response code="400">If the request is invalid</response>
    /// <response code="404">If the user is not found</response>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, UpdateUserRequest request)
    {
        var requestToUserResult = Models.User.From(id, request);

        if (requestToUserResult.IsError)
            return Problem(requestToUserResult.Errors);
        var user = requestToUserResult.Value;

        var updateUserResult = await _userService.UpdateUser(user);
        return updateUserResult.Match(_ => NoContent(), Problem);
    }

    /// <summary>
    /// Deletes a user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var deleteUserResult = await _userService.DeleteUser(id);
        return deleteUserResult.Match(_ => NoContent(), Problem);
    }

    private static UserResponse MapUserResponse(User user)
    {
        var response = new UserResponse(
            user.Id,
            user.ClerkId,
            user.FirstName,
            user.LastName,
            user.Bio,
            user.AccentColor,
            user.ImageUrl,
            user.Privacy,
            user.Role,
            user.CreatedAt
        );
        return response;
    }
}