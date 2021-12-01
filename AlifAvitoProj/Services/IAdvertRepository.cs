using AlifAvitoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Services
{
    public interface IAdvertRepository
    {
        ICollection<Advert> GetAdverts();
        Advert GetAdvert(int advertId);
        Advert GetAdvert(string advertTitle);
        bool AdvertExists(int advertId);
        bool AdvertExists(string advertTitle);
        bool IsDuplicateTitle(int advertId, string advertTitle);
        decimal GetAdvertRating(int advertId);

        bool CreateAdvert(int usersId, int categoriesId, Advert advert);
        bool UpdateAdvert(int usersId, int categoriesId, Advert advert);
        bool DeleteAdvert(Advert advert);
        bool Save();
    }
}
