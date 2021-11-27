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
        [HttpPost("users/CreateUser")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(User))]
        public async Task<IActionResult> CreateAuthor([FromBody] User userToCreate)
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

        //api/users/authorId
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
                ModelState.AddModelError("", "Author doesn't exist!");
            }

            if (!_cityRepository.CityExistsById(updatedUserInfo.City.Id))
            {
                ModelState.AddModelError("", "Country doesn't exist!");
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
                ModelState.AddModelError("", $"Something went wrong updating the author " + $"{updatedUserInfo.FirstName} and {updatedUserInfo.PhoneNumber}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
