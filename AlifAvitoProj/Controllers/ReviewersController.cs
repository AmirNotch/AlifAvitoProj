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
    public class ReviewersController : ControllerBase
    {
        private IReviewRepository _reviewRepository;
        private IAdvertRepository _advertRepository;
        private IReviewerRepository _reviewerRepository;

        public ReviewersController(IReviewRepository reviewRepository, IReviewerRepository reviewerRepository, IAdvertRepository advertRepository)
        {
            _reviewRepository = reviewRepository;
            _reviewerRepository = reviewerRepository;
            _advertRepository = advertRepository;
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

        //api/reviewers
        [HttpGet("GetReviewers")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        public async Task<IActionResult> GetReviewers()
        {
            var reviewers = _reviewerRepository.GetReviewers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewerDto = new List<ReviewerDto>();

            foreach (var reviewer in reviewers)
            {
                reviewerDto.Add(new ReviewerDto
                {
                    Id = reviewer.Id,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }

            return Ok(reviewerDto);
        }


        //api/reviewers/reviewerId
        [HttpPost("GetReviewer", Name = "GetReviewer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        public async Task<IActionResult> GetReviewer(string reviewerName)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerName))
            {
                return NotFound();
            }

            var reviewer = _reviewerRepository.GetReviewer(reviewerName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewerDto = new ReviewerDto
            {
                Id = reviewer.Id,
                FirstName = reviewer.FirstName,
                LastName = reviewer.LastName
            };

            return Ok(reviewerDto);
        }


        //api/reviewers/reviewerId/reviews
        [HttpGet("GetReviewsByReviewer/{reviewerId}/reviews")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public async Task<IActionResult> GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExistsById(reviewerId))
            {
                return NotFound();
            }

            var reviews = _reviewerRepository.GetReviewsByReviewer(reviewerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewsDto = new List<ReviewDto>();

            foreach (var review in reviews)
            {
                reviewsDto.Add(new ReviewDto
                {
                    Id = review.Id,
                    Headline = review.Headline,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating
                });
            }

            return Ok(reviewsDto);
        }


        //api/reviewers/reviewId/reviewer
        [HttpGet("GetReviewerOfAReview/{reviewId}/reviewer")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        public async Task<IActionResult> GetReviewerOfAReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }

            var reviewer = _reviewerRepository.GetReviewerOfAReview(reviewId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewerDto = new ReviewerDto
            {
                Id = reviewer.Id,
                FirstName = reviewer.FirstName,
                LastName = reviewer.LastName
            };

            return Ok(reviewerDto);
        }


        //api/reviewers
        [HttpPost("create")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(Reviewer))]
        public async Task<IActionResult> CreateReview([FromBody] Reviewer reviewerToCreate)
        {
            if (reviewerToCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewerRepository.CreateReviewer(reviewerToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving " +
                                            $"{reviewerToCreate.FirstName} and {reviewerToCreate.LastName}");
                return StatusCode(500, $"Something went wrong saving " +
                                            $"{reviewerToCreate.FirstName} and {reviewerToCreate.LastName}");
            }

            return CreatedAtRoute("GetReviewer", new { reviewerId = reviewerToCreate.Id }, reviewerToCreate);
        }


        //api/reviewers/reviewerId
        [HttpPut("update/{reviewerId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        public async Task<IActionResult> UpdateReviewer([FromRoute] int reviewerId, [FromBody] Reviewer updatedReviewerInfo)
        {

            updatedReviewerInfo.Id = reviewerId;

            if (updatedReviewerInfo == null)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewerRepository.ReviewerExistsById(reviewerId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewerRepository.UpdateReviewer(updatedReviewerInfo))
            {
                ModelState.AddModelError("", $"Something went wrong updating the reviewer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        //api/reviews
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public async Task<IActionResult> GetReviews()
        {
            var reviews = _reviewRepository.GetReviews();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewsDto = new List<ReviewDto>();

            foreach (var review in reviews)
            {
                reviewsDto.Add(new ReviewDto
                {
                    Id = review.Id,
                    Headline = review.Headline,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating
                });
            }

            return Ok(reviewsDto);
        }


        //api/reviews/reviewId
        [HttpGet("{reviewId}", Name = "GetReview")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public async Task<IActionResult> GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }

            var review = _reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewDto = new ReviewDto
            {
                Id = review.Id,
                Headline = review.Headline,
                ReviewText = review.ReviewText,
                Rating = review.Rating
            };

            return Ok(reviewDto);
        }


        //api/reviews/books/bookId
        [HttpGet("books/{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public async Task<IActionResult> GetReviewsOfABook(int bookId)
        {
            if (!_advertRepository.AdvertExists(bookId))
            {
                return NotFound();
            }

            var reviews = _reviewRepository.GetReviewsOfAdvert(bookId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewsDto = new List<ReviewDto>();

            foreach (var review in reviews)
            {
                reviewsDto.Add(new ReviewDto
                {
                    Id = review.Id,
                    Headline = review.Headline,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating
                });
            }

            return Ok(reviewsDto);
        }

        //api/reviews/reviewId/advert
        [HttpGet("{reviewId}/advert")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AdvertDto>))]
        public async Task<IActionResult> GetAdvertOfAReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }

            var advert = _reviewRepository.GetAdvertOfAReview(reviewId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var advertDto = new AdvertDto
            {
                Id = advert.Id,
                Title = advert.Title,
                ReviewText = advert.ReviewText,
                Cost = advert.Cost,
                DatePublished = advert.DatePublished
            };

            return Ok(advertDto);
        }


        //api/reviews
        [HttpPost("reviewCreate")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201, Type = typeof(Review))]
        public async Task<IActionResult> CreateReview([FromBody] Review reviewToCreate)
        {
            if (reviewToCreate == null)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewerRepository.ReviewerExistsById(reviewToCreate.Reviewer.Id))
            {
                ModelState.AddModelError("", "Reviewer doesn't exist!");
            }

            if (!_advertRepository.AdvertExists(reviewToCreate.Advert.Id))
            {
                ModelState.AddModelError("", "Book doesn't exist!");
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(404, ModelState);
            }

            reviewToCreate.Advert = _advertRepository.GetAdvert(reviewToCreate.Advert.Id);
            reviewToCreate.Reviewer = _reviewerRepository.GetReviewerById(reviewToCreate.Reviewer.Id);


            /*var country = _categoryRepository.GetCategories()
                            .Where(c => c.Name.Trim().ToUpper() == categoryToCreate.Name.Trim().ToUpper())
                            .FirstOrDefault();*/

            /*if (country != null)
            {
                ModelState.AddModelError("", $"Category {categoryToCreate.Name} alreade exists");
                return StatusCode(422, ModelState);
            }*/

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewRepository.CreateReview(reviewToCreate))
            {
                ModelState.AddModelError("", $"Something went wrong saving the review");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetReview", new { reviewId = reviewToCreate.Id }, reviewToCreate);
        }


        //api/reviews/reviewId
        [HttpPut("{reviewId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(Review))]
        public async Task<IActionResult> UpdateReview([FromRoute] int reviewId, [FromBody] Review updatedReviewInfo)
        {

            updatedReviewInfo.Id = reviewId;

            if (updatedReviewInfo == null)
            {
                return BadRequest(ModelState);
            }

            if (!_reviewerRepository.ReviewerExistsById(updatedReviewInfo.Reviewer.Id))
            {
                ModelState.AddModelError("", "Reviewer doesn't exist!");
            }

            if (!_advertRepository.AdvertExists(updatedReviewInfo.Advert.Id))
            {
                ModelState.AddModelError("", "Book doesn't exist!");
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(404, ModelState);
            }

            updatedReviewInfo.Advert = _advertRepository.GetAdvert(updatedReviewInfo.Advert.Id);
            updatedReviewInfo.Reviewer = _reviewerRepository.GetReviewerById(updatedReviewInfo.Reviewer.Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.UpdateReview(updatedReviewInfo))
            {
                ModelState.AddModelError("", $"Something went wrong updating the review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        //api/reviews/reviewId
        [HttpDelete("{reviewId}")]
        [ProducesResponseType(204)] // no content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }

            var deleteReview = _reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReview(deleteReview))
            {
                ModelState.AddModelError("", $"Something went wrong deleting {deleteReview.Headline}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
