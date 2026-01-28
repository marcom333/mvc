using Application.Entities;
namespace Application.Interface.Service;

public interface IUserService{
    public Task<User?> GetUser(int id);
}