using AlifAvitoProj.Context;
using AlifAvitoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlifAvitoProj.Services
{
    public class ReviewRepository : IReviewRepository
    {
        private AvitoDbContext _reviewContext;

        public ReviewRepository(AvitoDbContext avitoDbContext)
        {
            _reviewContext = avitoDbContext;
        }

        public bool CreateReview(Review review)
        {
            _reviewContext.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _reviewContext.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            throw new NotImplementedException();
        }

        public Advert GetAdvertOfAReview(int reviewId)
        {
            var advertId = _reviewContext.Reviews.Where(r => r.Id == reviewId).Select(b => b.Advert.Id).FirstOrDefault();
            return _reviewContext.Adverts.Where(b => b.Id == advertId).FirstOrDefault();
        }

        public Review GetReview(int reviewRating)
        {
            return _reviewContext.Reviews.Where(r => r.Id == reviewRating).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _reviewContext.Reviews.OrderBy(r => r.Rating).ToList();
        }

        public ICollection<Review> GetReviewsOfAdvert(int advertId)
        {
            return _reviewContext.Reviews.Where(r => r.Advert.Id == advertId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _reviewContext.Reviews.Any(r => r.Id == reviewId);
        }

        public bool Save()
        {
            var save = _reviewContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _reviewContext.Update(review);
            return Save();
        }
    }
}
