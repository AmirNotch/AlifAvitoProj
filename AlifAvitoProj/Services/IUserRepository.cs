using AlifAvitoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Services
{
    interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(string userFirstName);
        User GetUserById(int userid);
        ICollection<User> GetUsersOfAdvert(int advertId);
        ICollection<Advert> GetAdvertsByUser(int userId);
        bool UserExists(int userId);
        bool UserExistsByPhone(int userPhone);

        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
