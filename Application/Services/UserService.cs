using Application.Entities;
using Application.Interface.Service;
using Application.Interface.Repositories;

namespace Application.Services;

public class UserService : IUserService
{    
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUser(int id)
    {
        User? user = await _userRepository.GetUser(id);
        return user != null? user: new User();
    }

    public async Task<List<User>> GetUsers()
    {
        return await _userRepository.GetUsers();
    }
    
    public async Task<bool> CreateUser(User user)
    {
        return await _userRepository.CreateUser(user);
    }

    public async Task<bool> UpdateUser(User user)
    {
        return await _userRepository.UpdateUser(user);
    }
    
    public async Task<bool> DeleteUser(int id)
    {
        return await _userRepository.DeleteUser(id);
    }
}