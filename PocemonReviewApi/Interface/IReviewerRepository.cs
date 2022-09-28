using PocemonReviewApi.Models;

namespace PocemonReviewApi.Interface
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);

        ICollection<Review> GetReviewerById(int id);
        bool ReviewerExist(int reviewerid);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool Save();
    }
}
