using PocemonReviewApi.Models;

namespace PocemonReviewApi.Interface
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviwid);
        ICollection<Review> GetReviewsOfPokemon(int reviwid);
        bool ReviewExist(int id);

        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
        bool Save();

    }
}
