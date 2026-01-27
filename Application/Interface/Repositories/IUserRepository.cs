using Application.Entities;

namespace Application.Interface.Repositories;

public interface IUserRepository
{
    public Task<User> GetUser(int id);

    public Task<List<User>> GetUsers();

    public Task<bool> CreateUser(User user);
    
    public Task<bool> UpdateUser(User user);
    
    public Task<bool> DeleteUser(int id);
}