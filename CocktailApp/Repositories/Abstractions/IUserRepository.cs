using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Repositories.Abstractions;

public interface IUserRepository: IRepository<User>
{
    Task<ErrorOr<User>> GetByClerkId(string id);
}