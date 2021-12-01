using AlifAvitoProj.Context;
using AlifAvitoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Services
{
    public class AdvertRepository : IAdvertRepository
    {
        private AvitoDbContext _advertContext;

        public AdvertRepository(AvitoDbContext advertContext)
        {
            _advertContext = advertContext;
        }

        public bool AdvertExists(int advertId)
        {
            return _advertContext.Adverts.Any(b => b.Id == advertId);
        }

        public bool AdvertExists(string advertTitle)
        {
            return _advertContext.Adverts.Any(b => b.Title == advertTitle);
        }

        public bool CreateAdvert(int usersId, int categoriesId, Advert advert)
        {
            var users = _advertContext.Users.Where(a => usersId == a.Id).FirstOrDefault();
            var categories = _advertContext.Categories.Where(c => categoriesId == c.Id).FirstOrDefault();

           
                var advertUsers = new AdvertUser()
                {
                    User = users,
                    Advert = advert
                };
                _advertContext.Add(advertUsers);
            

            
                var advertCategory = new AdvertCategory()
                {
                    Category = categories,
                    Advert = advert
                };
                _advertContext.Add(advertCategory);
            

            _advertContext.AddAsync(advert);

            return Save();
        }

        public bool DeleteAdvert(Advert advert)
        {
            _advertContext.Remove(advert);
            return Save();
        }

        public Advert GetAdvert(int advertId)
        {
            return _advertContext.Adverts.Where(a => a.Id == advertId).FirstOrDefault();
        }

        public Advert GetAdvert(string advertTitle)
        {
            return _advertContext.Adverts.Where(a => a.Title == advertTitle).FirstOrDefault();
        }

        public decimal GetAdvertRating(int advertId)
        {
            var reviews = _advertContext.Reviews.Where(b => b.Advert.Id == advertId);
            if (reviews.Count() <= 0)
            {
                return 0;
            }

            return ((decimal)reviews.Sum(r => r.Rating) / reviews.Count());
        }

        public ICollection<Advert> GetAdverts()
        {
            return _advertContext.Adverts.OrderBy(b => b.Title).ToList();
        }

        public bool IsDuplicateTitle(int advertId, string advertTitle)
        {
            var advert = _advertContext.Adverts.Where(a => a.Title.Trim().ToUpper() == advertTitle.Trim().ToUpper()
                                                && a.Id != advertId).FirstOrDefault();
            return advert == null ? false : true;
        }

        public bool Save()
        {
            var save = _advertContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdateAdvert(int usersId, int categoriesId, Advert advert)
        {
            var users = _advertContext.Users.Where(a => usersId == a.Id).ToList();
            var categories = _advertContext.Categories.Where(c => categoriesId == c.Id).ToList();

            var advertUsersToDelete = _advertContext.AdvertUsers.Where(b => b.AdvertId == advert.Id);
            var advertCategoriesToDelete = _advertContext.AdvertCategories.Where(c => c.AdvertId == advert.Id);

            _advertContext.RemoveRange(advertUsersToDelete);
            _advertContext.RemoveRange(advertCategoriesToDelete);

            foreach (var user in users)
            {
                var advertUsers = new AdvertUser()
                {
                    User = user,
                    Advert = advert
                };
                _advertContext.Add(advertUsers);
            }

            foreach (var category in categories)
            {
                var advertCategory = new AdvertCategory()
                {
                    Category = category,
                    Advert = advert
                };
                _advertContext.Add(advertCategory);
            }

            _advertContext.Update(advert);

            return Save();
        }
    }
}
