using AlifAvitoProj.Context;
using AlifAvitoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Services
{
    public class UserRepository : IUserRepository
    {
        private AvitoDbContext _userContext;

        public UserRepository(AvitoDbContext userContext)
        {
            _userContext = userContext;
        }

        public bool CreateUser(User user)
        {
            _userContext.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _userContext.Remove(user);
            return Save();
        }

        public ICollection<Advert> GetAdvertsByUser(int userId)
        {
            return _userContext.AdvertUsers.Where(a => a.User.Id == userId).Select(b => b.Advert).ToList();
        }

        public ICollection<User> GetUsersOfAdvert(int advertId)
        {
            return _userContext.AdvertUsers.Where(b => b.Advert.Id == advertId).Select(a => a.User).ToList();
        }

        public ICollection<User> GetUsers()
        {
            return _userContext.Users.OrderBy(u => u.FirstName).ToList();
        }

        public bool Save()
        {
            var save = _userContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _userContext.Update(user);
            return Save();
        }

        public bool UserExists(int userId)
        {
            return _userContext.Users.Any(u => u.Id == userId);
        }

        public bool UserExistsByPhone(int userPhone)
        {
            return _userContext.Users.Any(u => u.PhoneNumber == userPhone);
        }

        public User GetUser(string userFirstName)
        {
            return _userContext.Users.Where(u => u.FirstName == userFirstName).FirstOrDefault();
        }

        public User GetUserById(int userid)
        {
            return _userContext.Users.Where(u => u.Id == userid).FirstOrDefault();
        }

        public User GetUserByPhone(int userPhone)
        {
            return _userContext.Users.Where(u => u.PhoneNumber == userPhone).FirstOrDefault();
        }
    }
}
