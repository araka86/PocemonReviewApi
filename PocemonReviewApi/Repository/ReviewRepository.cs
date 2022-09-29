using AutoMapper;
using PocemonReviewApi.Data;
using PocemonReviewApi.Interface;
using PocemonReviewApi.Models;

namespace PocemonReviewApi.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public ReviewRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();

        }

        public bool DeleteReviews(List<Review> reviews)
        {
           _context.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(int reviwid)
        {
            return _context.Reviews.Where(x => x.Id == reviwid).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            var ttttt = _context.Reviews.ToList();
            return _context.Reviews.ToList();
        }


        public ICollection<Review> GetReviewsOfPokemon(int reviwid)
        {
           
            return _context.Reviews.Where(x => x.Pokemon.Id == reviwid).ToList();
        }

        public bool ReviewExist(int reviwid)
        {
            return _context.Reviews.Any(x => x.Id == reviwid);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }
    }
}
