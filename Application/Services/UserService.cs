
using System.Threading.Tasks;
using Application.Entities;
using Application.Interface.Repositories;
using Application.Interface.Service;

namespace Application.Services;

public class UserService : IUserService {
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUser(User user) {
        return await _userRepository.CreateUser(user);
    }

    public async Task<User?> GetUser(int id) {
        return await _userRepository.GetUser(id);
    }

    public async Task<List<User>> GetUsers(string? name) {
        return await _userRepository.GetUsers(name);
    }

    public async Task UpdateUser(User user) {
        await _userRepository.UpdateUser(user);
    }

    public async Task DeleteUser(User user) {
        await _userRepository.DeleteUser(user);
    }
}
