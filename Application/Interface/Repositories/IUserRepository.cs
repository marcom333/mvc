using Application.Entities;

namespace Application.Interface.Repositories;

public interface IUserRepository{
    public Task<User> CreateUser(User u);
    public Task<User?> GetUser(int id);
    public Task<List<User>> GetUsers(string? name);
    public Task UpdateUser(User u);
    public Task DeleteUser(User u);
}