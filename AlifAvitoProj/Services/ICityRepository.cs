using AlifAvitoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Services
{
    public interface ICityRepository
    {
        ICollection<City> GetCities();
        City GetCity(string countryName);
        City GetCityById(int countryId);
        City GetCityOfAnUser(string userName);
        ICollection<User> GetUsersFromCity(int countryId);
        bool CityExists(string countryName);
        bool CityExistsById(int countryId);
        bool IsDuplicateCityName(string countryName, string cityName);
        bool IsDuplicateCityId(int countryId, string countryName);

        bool CreateCity(City city);
        bool UpdateCity(City city);
        bool DeleteCity(City city);
        bool Save();
    }
}
