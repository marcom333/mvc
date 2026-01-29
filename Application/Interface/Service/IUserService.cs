using Application.Entities;

namespace Application.Interface.Service;

public interface IUserService
{    
    public Task<User> GetUser(int id);

    public Task<List<User>> GetUsers();

    public Task<bool> CreateUser(User user);

    public Task<bool> UpdateUser(User user);

    public Task<bool> DeleteUser(int id);

    public Task<User> GetUserByEmail(string email);
}