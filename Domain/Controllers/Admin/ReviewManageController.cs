using Core.Dto.Review;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Models;

namespace Pizza.Controllers.Admin;

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
    [Route("Admin/EditReview")]
    public async Task<IActionResult> EditReview([FromQuery] int pizzaId, [FromQuery] int userId, [FromQuery] int orderId)
    {
        var review = await _reviewsService.GetReview(pizzaId, userId, orderId);
        if (review == null)
        {
            return NotFound();
        }

        return View("~/Views/Admin/Review/EditReview.cshtml", review);
    }


    [HttpPost]
    [Route("Admin/UpdateReview")]
    public async Task<IActionResult> UpdateReview(ReviewDto review)
    {
        if (ModelState.IsValid)
        {
            var success = await _reviewsService.UpdateReview(review);
            if (success)
            {
                return RedirectToAction("ManageReview");
            }
            ModelState.AddModelError(string.Empty, "Не удалось обновить отзыв.");
        }
        return View("~/Views/Admin/Review/EditReview.cshtml");
    }


    [HttpPost]
    [Route("Admin/DeleteReview")]
    public async Task<IActionResult> DeleteReview([FromQuery] int pizzaId, [FromQuery] int userId, [FromQuery] int orderId)
    {
        var success = await _reviewsService.DeleteReview(pizzaId, userId, orderId);
        if (success)
        {
            return RedirectToAction("ManageReview");
        }
        ModelState.AddModelError(string.Empty, "Не удалось удалить отзыв.");
        return RedirectToAction("ManageReview");
    }


    [HttpGet]
    [Route("Admin/AddReview")]
    public IActionResult AddReview()
    {
        return View("~/Views/Admin/Review/AddReview.cshtml");
    }

    [HttpPost]
    [Route("Admin/AddReview")]
    public async Task<IActionResult> AddReview(ReviewDto review)
    {
        if (ModelState.IsValid)
        {
            var success = await _reviewsService.AddReview(review);
            if (success)
            {
                return RedirectToAction("ManageReview");
            }
            ModelState.AddModelError(string.Empty, "Failed to add review.");
        }
        return View("~/Views/Admin/Review/AddReview.cshtml");
    }
}
