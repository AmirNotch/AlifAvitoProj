using AlifAvitoProj.Dto;
using AlifAvitoProj.Models;
using AlifAvitoProj.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private ICityRepository _cityRepository;

        public AdminsController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        //api/admins
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
        public IActionResult GetCities()
        {
            var countries = _cityRepository.GetCities().ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countriesDto = new List<CountryDto>();
            foreach (var country in countries)
            {
                countriesDto.Add(new CountryDto
                {
                    Id = country.Id,
                    Name = country.Name,
                });
            }

            return Ok(countriesDto);
        }


        //api/admins
        [HttpPost("GetCity", Name = "GetCountry")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
        public IActionResult GetCity(City city)
        {
            if (!_cityRepository.CityExists(city.Name))
                return NotFound();

            var country = _cityRepository.GetCity(city.Name);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryDto = new CountryDto()
            {
                Id = country.Id,
                Name = country.Name
            };

            return Ok(countryDto);
        }


        //api/admins/users
        [HttpPost("admins/users")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
        public async Task<IActionResult> GetCityOfAnUser(string firstName)
        {
            /*if (!_cityRepository.(authorId))
            {
                return NotFound();
            }*/

            var country = _cityRepository.GetCityOfAnUser(firstName);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryDto = new CountryDto()
            {
                Id = country.Id,
                Name = country.Name
            };

            return Ok(countryDto);
        }

        //api/admins/city
        [HttpPost("city")]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(City))]
        public async Task<IActionResult> CreateCountry([FromBody] City cityToCreate)
        {
            if (cityToCreate == null)
            {
                return BadRequest(ModelState);
            }

            var city = _cityRepository.GetCities()
                            .Where(c => c.Name.Trim().ToUpper() == cityToCreate.Name.Trim().ToUpper())
                            .FirstOrDefault();

            if (city != null)
            {
                ModelState.AddModelError("", $"City {cityToCreate.Name} alreade exists");
                return StatusCode(422, $"City {cityToCreate.Name} alreade exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_cityRepository.CreateCity(cityToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {cityToCreate.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCountry", new { countryId = cityToCreate.Id }, cityToCreate);
        }


        //api/admins/cityId
        [HttpPut("{cityId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(City))]
        public async Task<IActionResult> UpdateCountry(int cityId, [FromBody] City updatedCityInfo)
        {
            updatedCityInfo.Id = cityId;
            //updatedCountryInfo.Id = cityName;

            if (updatedCityInfo == null)
            {
                return BadRequest(ModelState);
            }

            /*if (countryId != updatedCountryInfo.Id)
            {
                return BadRequest(ModelState);
            }*/

            if (!_cityRepository.CityExistsById(cityId))
            {
                return NotFound();
            }

            if (_cityRepository.IsDuplicateCityId(cityId, updatedCityInfo.Name))
            {
                ModelState.AddModelError("", $"City {updatedCityInfo.Name} alreade exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_cityRepository.UpdateCity(updatedCityInfo))
            {
                ModelState.AddModelError("", $"Something went wrong updating {updatedCityInfo.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //api/admins/cityId
        [HttpDelete("{cityId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult DeleteCountry(int cityId)
        {
            if (!_cityRepository.CityExistsById(cityId))
            {
                return NotFound();
            }

            var deleteCountry = _cityRepository.GetCityById(cityId);

            if (_cityRepository.GetUsersFromCity(cityId).Count() > 0)
            {
                ModelState.AddModelError("", $"Country {deleteCountry.Name} " +
                                                                "can not be deleted because authors used this country");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_cityRepository.DeleteCity(deleteCountry))
            {
                ModelState.AddModelError("", $"Something went wrong deleting {deleteCountry.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
