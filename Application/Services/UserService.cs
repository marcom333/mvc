using Application.Entities;
using Application.Interface.Service;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private List<User> Users;

        public UserService()
        {
            Users = new List<User>()
        {
            new User()
            {
                Name = "Paco",
                Primer_Apellido = "Paco",
                Segundo_Apellido = "Paco",
            },
            new User()
            {
                Name = "Luis",
                Primer_Apellido = "Luis",
                Segundo_Apellido = "Luis",
            },
           
        };
        }

        public User GetUser(int id)
        {
            //dapper where id = id
            User user = new User()
            {
                Name = "Pedro",
                Primer_Apellido = "Ypablo",
                Segundo_Apellido = "EranHermanos",
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
}
