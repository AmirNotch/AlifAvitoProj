using AlifAvitoProj.Dto;
using AlifAvitoProj.Models;
using AlifAvitoProj.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlifAvitoProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;
        private ICityRepository _cityRepository;
        private IAdvertRepository _advertRepository;
        private ICategoryRepository _categoryRepository;
        private IReviewRepository _reviewRepository;

        public UsersController(IUserRepository userRepository, ICityRepository cityRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
        }

        /*//api/authors/authorId
        [HttpPost("user/authorId", Name = "GetUsers")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
        public async Task<IActionResult> GetUser(int userPhone)
        {
            if (!_userRepository.UserExistsByPhone(userPhone))
            {
                return NotFound();
            }

            var user = _userRepository.GetUserByPhone(userPhone);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDto userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                PhoneNumber = user.PhoneNumber
            };

            return Ok(userDto);
        }*/


        //api/users
        [HttpGet("users/Check")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(User))]
        public async Task<IActionResult> CreateUser([FromRoute] int userPhone)
        {

            if (!_userRepository.UserExistsByPhone(userPhone))
            {
                ModelState.AddModelError("", "User doesn't exist!");
            }

            return Ok("User exist");
        }

        //api/users
        [HttpPost("users/CreateUser")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(User))]
        public async Task<IActionResult> CreateUser([FromBody] User userToCreate)
        {
            
            if (userToCreate == null)
            {
                return BadRequest(ModelState);
            }
            
            using (SHA1 sha1Hash = SHA1.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(userToCreate.Password);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                userToCreate.Password = hash;
            }

            if (!_cityRepository.CityExistsById(userToCreate.City.Id))
            {
                ModelState.AddModelError("", "City doesn't exist!");
            }

            userToCreate.City = _cityRepository.GetCityById(userToCreate.City.Id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.CreateUser(userToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving the author " + $"{userToCreate.FirstName} and {userToCreate.PhoneNumber}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetUsers", new { userId = userToCreate.Id }, userToCreate);
        }


        //api/users/userId
        [HttpPut("users/{userId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(User))]
        public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] User updatedUserInfo)
        {
            updatedUserInfo.Id = userId;

            if (updatedUserInfo == null)
            {
                return BadRequest(ModelState);
            }

            if (!_userRepository.UserExists(userId))
            {
                ModelState.AddModelError("", "User doesn't exist!");
            }

            if (!_cityRepository.CityExistsById(updatedUserInfo.City.Id))
            {
                ModelState.AddModelError("", "City doesn't exist!");
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(404, ModelState);
            }

            updatedUserInfo.City = _cityRepository.GetCityById(updatedUserInfo.City.Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.UpdateUser(updatedUserInfo))
            {
                ModelState.AddModelError("", $"Something went wrong updating the user " + $"{updatedUserInfo.FirstName} and {updatedUserInfo.PhoneNumber}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        //api/adverts
        [HttpGet("adverts")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AdvertDto>))]
        public async Task<IActionResult> GetAdverts()
        {
            var adverts = _advertRepository.GetAdverts();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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


        //api/adverts/advertId
        [HttpPost("advertByTitle",Name = "GetAdvert")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AdvertDto>))]
        public async Task<IActionResult> GetAdvertByTitle(string advertTitle)
        {
            if (!_advertRepository.AdvertExists(advertTitle))
            {
                return NotFound();
            }
            var adverts = _advertRepository.GetAdvert(advertTitle);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var advertsDto = new AdvertDto
            {
                Id = adverts.Id,
                Title = adverts.Title,
                ReviewText = adverts.ReviewText,
                Cost = adverts.Cost,
                DatePublished = adverts.DatePublished
            };

            return Ok(advertsDto);
        }

        //api/adverts/Id/advertId
        [HttpGet("Id/{advertId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AdvertDto>))]
        public async Task<IActionResult> GetAdvertById(int advertId)
        {
            if (!_advertRepository.AdvertExists(advertId))
            {
                return NotFound();
            }
            var adverts = _advertRepository.GetAdvert(advertId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var advertDto = new AdvertDto
            {
                Id = adverts.Id,
                Title = adverts.Title,
                ReviewText = adverts.ReviewText,
                Cost = adverts.Cost,
                DatePublished = adverts.DatePublished
            };

            return Ok(advertDto);
        }

        //api/adverts/advertId/rating
        [HttpGet("{advertId}/rating")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(decimal))]
        public async Task<IActionResult> GetAdvertRating(int bookId)
        {
            if (!_advertRepository.AdvertExists(bookId))
            {
                return NotFound();
            }
            var book = _advertRepository.GetAdvertRating(bookId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(book);
        }

        //api/adverts?authId=1&authId=2&catId=1&catId=2
        [HttpPost("adverts")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(Advert))]
        public IActionResult CreateAdvert([FromRoute] int userId, [FromRoute] int catId,
                                        [FromBody] Advert advertToCreate)
        {
            advertToCreate.DatePublished = DateTime.UtcNow;
            var statusCode = ValidateAdvert(userId, catId, advertToCreate);

            if (!ModelState.IsValid)
            {
                return StatusCode(statusCode.StatusCode);
            }

            if (!_advertRepository.CreateAdvert(userId, catId, advertToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving the advert " +
                                             $"{advertToCreate.Title}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetBook", new { advertId = advertToCreate.Id }, advertToCreate);
        }


        //api/adverts/advertId?authId=1&authId=2&catId=1&catId=2
        [HttpPut("{advertId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        [ProducesResponseType(204)]
        public IActionResult UpdateAdvert(int advertId, [FromQuery] int userId, [FromQuery] int catId,
                                        [FromBody] Advert advertToUpdate)
        {
            advertToUpdate.DatePublished = DateTime.UtcNow;
            var statusCode = ValidateAdvert(userId, catId, advertToUpdate);

            advertToUpdate.Id = advertId;

            if (!_advertRepository.AdvertExists(advertId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(statusCode.StatusCode);
            }

            if (!_advertRepository.UpdateAdvert(userId, catId, advertToUpdate))
            {
                ModelState.AddModelError("", $"Something went wrong updating the advert " +
                                             $"{advertToUpdate.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        //api/adverts/advertId
        [HttpDelete("{advertId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(422)]
        [ProducesResponseType(204)]
        public IActionResult DeleteAdvert(int advertId)
        {

            if (!_advertRepository.AdvertExists(advertId))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetReviewsOfAdvert(advertId);
            var advertToDelete = _advertRepository.GetAdvert(advertId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", $"Something went wrong deleting reviews");
                return StatusCode(500, ModelState);
            }

            if (!_advertRepository.DeleteAdvert(advertToDelete))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the advert {advertToDelete.Title}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }





        private StatusCodeResult ValidateAdvert(int userId, int catId, Advert advert)
        {
            if (advert == null || userId <= 0 || catId <= 0)
            {
                ModelState.AddModelError("", "Missing book, author, or category");
                return BadRequest();
            }

            /*if (_advertRepository.IsDuplicateTitle(advert.Id, advert.Title))
            {
                ModelState.AddModelError("", "Duplicate Title");
                return StatusCode(422);
            }*/

            
            /*if (!_categoryRepository.CategoryExistsById(userId))
            {
                ModelState.AddModelError("", "Category not found");
                return StatusCode(404);
            }*/
            

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Critical Error");
                return BadRequest();
            }

            return NoContent();
        }
    }
}