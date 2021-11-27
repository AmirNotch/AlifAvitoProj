﻿using AlifAvitoProj.Dto;
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
        private ICategoryRepository _categoryRepository;

        public AdminsController(ICityRepository cityRepository, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
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

        /////////////////////////////////
        ///   Categories
        ////////////////////////////////


        //api/admins/categories
        [HttpGet("categories")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories().ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriesDto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoriesDto.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                });
            }

            return Ok(categoriesDto);
        }

        //api/categories/category
        [HttpPost("GetCategory", Name = "GetCategory")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategory(string categoryName)
        {
            if (!_categoryRepository.CategoryExists(categoryName))
                return NotFound();

            var category = _categoryRepository.GetCategory(categoryName);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryDto = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(categoryDto);
        }


        //api/categories
        [HttpPost("categoryName")]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(Category))]
        public async Task<IActionResult> CreateCategory([FromBody] Category categoryToCreate)
        {
            if (categoryToCreate == null)
            {
                return BadRequest(ModelState);
            }

            var country = _categoryRepository.GetCategories()
                            .Where(c => c.Name.Trim().ToUpper() == categoryToCreate.Name.Trim().ToUpper())
                            .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", $"Category {categoryToCreate.Name} alreade exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_categoryRepository.CreateCategory(categoryToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving {categoryToCreate.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategory", new { categoryId = categoryToCreate.Id }, categoryToCreate);
        }


        //api/admins/categoryId
        [HttpPut("put/{categoryId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(Category))]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody]Category updatedCategoryInfo)
        {
            /*Category category = new Category()
            {
                Id = categoryId, 
                Name = updatedCategoryInfo   
            };*/
            updatedCategoryInfo.Id = categoryId;

            if (updatedCategoryInfo == null)
            {
                return BadRequest(ModelState);
            }

            /*if (countryId != updatedCountryInfo.Id)
            {
                return BadRequest(ModelState);
            }*/

            if (!_categoryRepository.CategoryExistsById(categoryId))
            {
                return NotFound();
            }

            if (_categoryRepository.IsDuplicateCategoryById(categoryId, updatedCategoryInfo.Name))
            {
                ModelState.AddModelError("", $"Category {updatedCategoryInfo} alreade exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.UpdateCategory(updatedCategoryInfo))
            {
                ModelState.AddModelError("", $"Something went wrong updating {updatedCategoryInfo.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        //api/categories/categoryId/books
        [HttpGet("{categoryId}/books")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AdvertDto>))]
        public async Task<IActionResult> GetAllAdvertsForACategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExistsById(categoryId))
                return NotFound();

            var adverts = _categoryRepository.GetAllAdvertsForCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var advertsDto = new List<AdvertDto>();

            foreach (var advert in adverts)
            {
                advertsDto.Add(new AdvertDto
                {
                    Id = advert.Id, 
                    Title = advert.Title,
                    ReviewText = advert.ReviewText,
                    Cost = advert.Cost,
                    DatePublished = advert.DatePublished
                });
            }

            return Ok(advertsDto);
        }

        //api/categories/books/bookId
        [HttpGet("advertss/{advertId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public async Task<IActionResult> GetAllCategoriesForAdvert(int advertId)
        {
            /*if (!_bookRepository.BookExists(bookId))
            {
                return NotFound();
            }*/
            var categories = _categoryRepository.GetAllCategoriesForAdvert(advertId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriesDto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoriesDto.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return Ok(categoriesDto);
        }


        //api/admins/categoryId
        [HttpDelete("delete/{categoryId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExistsById(categoryId))
            {
                return NotFound();
            }

            var deleteCategory = _categoryRepository.GetCategoryById(categoryId);

            if (_categoryRepository.GetAllAdvertsForCategory(categoryId).Count() > 0)
            {
                ModelState.AddModelError("", $"Category {deleteCategory.Name} " +
                                                                "can not be deleted because books used this category");
                return StatusCode(409, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.DeleteCategory(deleteCategory))
            {
                ModelState.AddModelError("", $"Something went wrong deleting {deleteCategory.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
