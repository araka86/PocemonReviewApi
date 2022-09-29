using PocemonReviewApi.Models;

namespace PocemonReviewApi.Interface
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);

        ICollection<Review> GetReviewsByReviewer(int reviewerId);
        bool ReviewerExist(int reviewerid);
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewerid);
        bool Save();
    }
}
