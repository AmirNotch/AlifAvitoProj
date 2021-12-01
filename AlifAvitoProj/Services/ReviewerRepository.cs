using AlifAvitoProj.Context;
using AlifAvitoProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlifAvitoProj.Services
{
    public class ReviewerRepository : IReviewerRepository
    {
        private AvitoDbContext _reviewerContext;
        
        public ReviewerRepository(AvitoDbContext reviewerContext)
        {
            _reviewerContext = reviewerContext;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _reviewerContext.AddAsync(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _reviewerContext.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(string reviewerName)
        {
            return _reviewerContext.Reviewers.Where(r => r.FirstName.Contains(reviewerName)).FirstOrDefault();
        }

        public Reviewer GetReviewerOfAReview(int reviewId)
        {
            var reviewerId = _reviewerContext.Reviews.Where(r => r.Id == reviewId).Select(rr => rr.Reviewer.Id).FirstOrDefault();
            return _reviewerContext.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _reviewerContext.Reviewers.OrderBy(r => r.FirstName).ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerName)
        {
            return _reviewerContext.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(string reviewerName)
        {
            return _reviewerContext.Reviewers.Any(r => r.FirstName == reviewerName);
        }

        public bool ReviewerExistsById(int reviewerInt)
        {
            return _reviewerContext.Reviewers.Any(r => r.Id == reviewerInt);
        }

        public bool Save()
        {
            var save = _reviewerContext.SaveChanges();
            return save >= 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _reviewerContext.Update(reviewer);
            return Save();
        }
    }
}
