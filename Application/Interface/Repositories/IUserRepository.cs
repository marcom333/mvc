
using Application.Entities;

namespace Application.Interface.Repositories;

public interface IUserRepository {
    public Task<List<User>> GetUsers();
    
    public Task<User?> GetUser(int id);

    public Task DeleteUser(User u);

    public Task<User> CreateUser(User u);

    public Task UpdateUser(User u);
}