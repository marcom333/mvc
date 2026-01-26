using Application.Entities;
using Application.Interface.Service;

namespace Application.Services;

public class UserService : IUserService
{
    
    private List<User> Users;

    public UserService()
    {
        Users = new List<User>()
        {
            new User()
            {
                Name = "César Javier",
                Primer_Apellido = "Maldonado",
                Segundo_Apellido = "Flores",
            },
            new User()
            {
                Name = "Marco",
                Primer_Apellido = "Trujillo",
                Segundo_Apellido = "Castillo",
            },
            new User()
            {
                Name = "Rosa",
                Primer_Apellido = "Del Valle",
                Segundo_Apellido = "Dolores",
            },
            new User()
            {
                Name = "Sergio",
                Primer_Apellido = "Monarca",
                Segundo_Apellido = "Talamantes",
            }
        };    
    }

    public User GetUser(int id)
    {
        //dapper where id = id
        User user = new User()
        {
            Name = "Monica",
            Primer_Apellido = "Rivera",
            Segundo_Apellido = "Rivera",
        };

        return user;
    }

    public List<User> GetUsers()
    {
        return this.Users;
    }
    
    public bool CreateUser(User user)
    {
        //dapper Saving
        User newUser = user;
        return newUser != null;
    }

    public bool UpdateUser(User user)
    {
        //dapper Updating
        User User = user;
        return User != null;
    }
}