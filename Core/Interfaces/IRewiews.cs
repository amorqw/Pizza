using Core.Dto.Review;
using Core.Models;

namespace Core.Interfaces;

public interface IReviews
{
    Task<IEnumerable<Reviews>> GetAllReviews();
    Task<Reviews?> GetReview(int pizzaId, int userId, int orderId); 
    Task<IEnumerable<Reviews>> GetReviewsByPizzaId(int pizzaId); 
    Task<IEnumerable<Reviews>> GetReviewsByUserId(int userId); 
    Task<bool> AddReview(ReviewDto review); 
    Task<bool> UpdateReview(ReviewDto review); 
    Task<bool> DeleteReview(int pizzaId, int userId, int orderId); 
}