using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PocemonReviewApi.Data;
using PocemonReviewApi.Interface;
using PocemonReviewApi.Models;

namespace PocemonReviewApi.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public ReviewerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int reviewerid)
        {
            return _context.Reviewers.Where(x => x.Id == reviewerid).Include(y => y.Reviews).FirstOrDefault();
        }

        public ICollection<Review> GetReviewerById(int id)
        {
           return _context.Reviews.Where(r=>r.Reviewer.Id == id).ToList();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public bool ReviewerExist(int reviewerid)
        {
            return _context.Reviews.Any(r => r.Id == reviewerid);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
