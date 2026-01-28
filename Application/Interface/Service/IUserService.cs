using Application.Entities;
namespace Application.Interface.Service;

public interface IUserService
{
    public Task<User> CreateUser(User user);
    public Task<User?> GetUser(int id);
    public Task<List<User>> GetUsers(string? name);
    public Task UpdateUser(User user);
    public Task DeleteUser(User user);
}