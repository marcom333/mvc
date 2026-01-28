using Application.Entities;

namespace Application.Interface.Repositories;

public interface IUserRepository{
    public Task<User?> GetUser(int id);
}