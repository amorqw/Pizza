using Microsoft.AspNetCore.Mvc;
using Core.Models;
using Core.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviews _reviewsService;

        public ReviewsController(IReviews reviewsService)
        {
            _reviewsService = reviewsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reviews>> GetReview(int id)
        {
            var review = await _reviewsService.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpGet("pizza/{pizzaId}")]
        public async Task<ActionResult<IEnumerable<Reviews>>> GetReviewsByPizzaId(int pizzaId)
        {
            var reviews = await _reviewsService.GetReviewsByPizzaId(pizzaId);
            return Ok(reviews);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Reviews>>> GetReviewsByUserId(int userId)
        {
            var reviews = await _reviewsService.GetReviewsByUserId(userId);
            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReview(Reviews review)
        {
            var result = await _reviewsService.AddReview(review);
            if (!result)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetReview), new { id = review.ReviewId }, review);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReview(int id, Reviews review)
        {
            if (id != review.ReviewId)
            {
                return BadRequest();
            }

            var result = await _reviewsService.UpdateReview(review);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            var result = await _reviewsService.DeleteReview(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}