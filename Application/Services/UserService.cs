using Application.Interface.Service;
using Application.Entities;

namespace Application.Services;

public class UserService : IUserService
{
    private List<User>Users;
    public User? GetUser(int userid)
    {
        foreach(User user in Users)
        {
            if(user.UserId == userid)
            {
                return user;
            }
        }
        return null;
    }

    public List<User> GetUsers()
    {
        return Users;
    }
    public void CreateUser(User user)
    {
        Users.Add(user);
    }
    public void UpdateUser(User user)
    {
        foreach(User existing_user in Users)
        {
            if(user.UserId == existing_user.UserId)
            {
                user = existing_user;
            }
        }
    }


    public UserService()
    {
        Users = [
            new User()
            {
                Name = "Juan Perez",
                UserId = 1
            },
            new User()
            {
                Name = "José Martinez",
                UserId = 2
            },
            new User()
            {
                Name = "Yolanda Hernández",
                UserId = 3
            }
        ];
    }
}