using Application.Entities;

namespace Application.Interface.Service;

public interface IUserService
{
    public User? GetUser(int ind);
    public List<User>GetUsers();
    public void CreateUser(User user);
    public void UpdateUser(User user);
}