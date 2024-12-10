using Core.Models;

namespace Core.Interfaces;

public interface IReviews
{
    Task<Reviews?> GetReviewById(int reviewId); 
    Task<IEnumerable<Reviews>> GetReviewsByPizzaId(int pizzaId); 
    Task<IEnumerable<Reviews>> GetReviewsByUserId(int userId); 
    Task<bool> AddReview(Reviews review); 
    Task<bool> UpdateReview(Reviews review); 
    Task<bool> DeleteReview(int reviewId); 
}