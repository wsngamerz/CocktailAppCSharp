using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Repositories.Abstractions;

public interface IUserRepository
{
    Task<ErrorOr<Created>> Create(User user);
    Task<ErrorOr<User>> GetById(Guid id);
    Task<ErrorOr<User>> GetByClerkId(string id);
    Task<ErrorOr<IEnumerable<User>>> GetMany();
    Task<ErrorOr<Updated>> Update(User user);
    Task<ErrorOr<Deleted>> Delete(Guid id);
    Task<ErrorOr<int>> Count();
}