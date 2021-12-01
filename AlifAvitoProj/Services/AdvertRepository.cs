using AlifAvitoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Services
{
    public class AdvertRepository : IAdvertRepository
    {
        public bool AdvertExists(int advertId)
        {
            throw new NotImplementedException();
        }

        public bool AdvertExists(string advertTitle)
        {
            throw new NotImplementedException();
        }

        public bool CreateAdvert(int advertsId, int categoriesId, Advert advert)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAdvert(Advert advert)
        {
            throw new NotImplementedException();
        }

        public decimal GetAdvertRating(int advertId)
        {
            throw new NotImplementedException();
        }

        public Advert GetBook(int advertId)
        {
            throw new NotImplementedException();
        }

        public Advert GetBook(string advertTitle)
        {
            throw new NotImplementedException();
        }

        public ICollection<Advert> GetBooks()
        {
            throw new NotImplementedException();
        }

        public bool IsDuplicateTitle(int advertId, string advertTitle)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateAdvert(int advertsId, int categoriesId, Advert advert)
        {
            throw new NotImplementedException();
        }
    }
}
