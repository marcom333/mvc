using Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IUserService
    {
        public User GetUser(int id);

        public List<User> GetUsers();

        public bool CreateUser(User user);

        public bool UpdateUser(User user);
    }
}
