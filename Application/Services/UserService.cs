using Application.Entities;
using Application.Interface.Repositories;

namespace Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository){
        _userRepository = userRepository;
    }

    public async Task<User?> GetUser(int id)
    {
        return await _userRepository.GetUser(id);
    }
}