using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Services.Abstractions;

public interface IUserService
{
    Task<ErrorOr<Created>> CreateUser(User user);
    Task<ErrorOr<User>> GetUser(Guid id);
    Task<ErrorOr<IEnumerable<User>>> GetUsers();
    Task<ErrorOr<Updated>> UpdateUser(User user);
    Task<ErrorOr<Deleted>> DeleteUser(Guid id);
    Task<ErrorOr<string>> LoginUser(string email, string password);
}