using Application.Entities;

namespace Application.Interface.Service;

public interface IUserService
{    
    public User GetUser(int id);

    public List<User> GetUsers();

    public bool CreateUser(User user);

    public bool UpdateUser(User user);
}