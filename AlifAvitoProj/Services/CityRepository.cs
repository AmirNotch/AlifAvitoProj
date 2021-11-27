using AlifAvitoProj.Context;
using AlifAvitoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Services
{
    public class CityRepository : ICityRepository
    {
        private AvitoDbContext _avitoDbContext;

        public CityRepository(AvitoDbContext avitoDbContext)
        {
            _avitoDbContext = avitoDbContext;
        }

        public bool CityExists(string countryName)
        {
            return _avitoDbContext.Cities.Any(c => c.Name == countryName);
        }

        public bool CreateCity(City city)
        {
            _avitoDbContext.AddAsync(city);
            return Save();
        }

        public bool DeleteCity(City city)
        {
            _avitoDbContext.Remove(city);
            return Save();
        }

        public ICollection<City> GetCities()
        {
            return _avitoDbContext.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(string countryName)
        {
            return _avitoDbContext.Cities.Where(c => c.Name.Contains(countryName)).FirstOrDefault();
        }

        public City GetCityOfAnUser(int authorId)
        {
            throw new NotImplementedException();
        }

        public ICollection<City> GetUsersFromCity(int countryId)
        {
            throw new NotImplementedException();
        }

        public bool IsDuplicateCityName(string countryName)
        {
            var country = _avitoDbContext.Cities.Where(c => c.Name.Trim().ToUpper() == countryName.Trim().ToUpper()).FirstOrDefault();
            return country == null ? false : true;
        }

        public bool Save()
        {
            var save = _avitoDbContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdateCity(City city)
        {
            _avitoDbContext.Update(city);
            return Save();
        }
    }
}
