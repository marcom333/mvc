

using Application.Entities;

namespace Application.Interface.Service;

public interface IUserService {
    
    public Task<User?> GetUser(int ind);
    public Task<List<User>> GetUsers();
    public Task<User> CreateUser(User model);
    public Task UpdateUser(User model);
    public Task DeleteUser(User model);

}