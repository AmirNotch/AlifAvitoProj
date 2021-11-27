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
        City GetCity(string countryName;
        City GetCityOfAnUser(int authorId);
        ICollection<City> GetUsersFromCity(int countryId);
        bool CityExists(string countryName);
        bool IsDuplicateCityName(string countryName);

        bool CreateCity(City city);
        bool UpdateCity(City city);
        bool DeleteCity(City city);
        bool Save();
    }
}
