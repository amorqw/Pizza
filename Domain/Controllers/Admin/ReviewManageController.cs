using Core.Dto;
using Microsoft.AspNetCore.Mvc;
using Core.Dto.Pizza;
using Core.Dto.Review;
using Core.Interfaces;
using Core.Models;

namespace Pizza.Controllers.Admin
{
    public class ReviewManageController : Controller
    {
        private readonly IReviews _reviewsService;

        public ReviewManageController(IReviews reviewsService)
        {
            _reviewsService = reviewsService;
        }

        [HttpGet]
        [Route("Admin/ManageReview")]
        public async Task<IActionResult> ManageReview()
        {
            var reviews = await _reviewsService.GetAllReviews();
            return View("~/Views/Admin/Review/ManageReview.cshtml", reviews);
        }

        [HttpGet]
        [Route("Admin/EditReview/{id}")]
        public async Task<IActionResult> EditReview(int id)
        {
            var review = await _reviewsService.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }

            var reviewDto = new ReviewDto()
            {
                 
                 ReviewId = review.ReviewId,
                PizzaId = review.PizzaId,
                UserId = review.UserId,
                Comment = review.Comment,
                Rating = review.Rating,
            };

            return View("~/Views/Admin/Review/EditReview.cshtml", reviewDto);
        }

        [HttpPost]
        [Route("Admin/UpdateReview/{id}")]
        public async Task<IActionResult> UpdateReview(Reviews staffDto, int id)
        {
            if (ModelState.IsValid)
            {
                var updateStaffs = await _reviewsService.UpdateReview(staffDto);
                if (updateStaffs != null)
                {
                    return RedirectToAction("ManageReview");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update pizza.");
                }
            }
            return View("~/Views/Admin/Review/EditReview.cshtml");
        }

        [HttpGet]
        [Route("Admin/AddRevview")]
        public IActionResult AddReview()
        {
            var pizzaDto = new PizzaDto();  
            return View("~/Views/Admin/Review/AddReview.cshtml"); 
        }

        [HttpPost]
        [Route("Admin/AddReview")]
        public async Task<IActionResult> AddReview(Reviews staffDto)
        {
            if (ModelState.IsValid)
            {
                var newStaff = await _reviewsService.AddReview(staffDto);
                if (newStaff != null)
                {
                    return RedirectToAction("ManageReview");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to add staff");
                }
            }
            return View("~/Views/Admin/Review/AddReview.cshtml");
        }

        [HttpPost]
        [Route("Admin/DeleteReview/{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            var success = await _reviewsService.DeleteReview(id);
            if (success)
            {
                return RedirectToAction("ManageReview");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete pizza");
                return RedirectToAction("ManageReview");
            }
        }
    }
}
