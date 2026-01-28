using Application.Entities;
using Application.Interface.Repositories;
using Application.Interface.Service;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository){
        _userRepository = userRepository;
    }

    public async Task<User?> GetUser(int id)
    {
        return await _userRepository.GetUser(id);
    }

    public async Task<User> CreateUser(User User)
    {
        return await _userRepository.CreateUser(User);
    }

    public async Task<List<User>> GetUsers(string? name) {
        return await _userRepository.GetUsers(name);
    }

    public async Task UpdateUser(User User){
        await _userRepository.UpdateUser(User);
    }

    public async Task DeleteUser(User User) {
        await _userRepository.DeleteUser(User);
    }
}