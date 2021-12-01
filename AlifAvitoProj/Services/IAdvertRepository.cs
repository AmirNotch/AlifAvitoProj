using AlifAvitoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Services
{
    public interface IAdvertRepository
    {
        ICollection<Advert> GetBooks();
        Advert GetBook(int advertId);
        Advert GetBook(string advertTitle);
        bool AdvertExists(int advertId);
        bool AdvertExists(string advertTitle);
        bool IsDuplicateTitle(int advertId, string advertTitle);
        decimal GetAdvertRating(int advertId);

        bool CreateAdvert(int advertsId, int categoriesId, Advert advert);
        bool UpdateAdvert(int advertsId, int categoriesId, Advert advert);
        bool DeleteAdvert(Advert advert);
        bool Save();
    }
}
