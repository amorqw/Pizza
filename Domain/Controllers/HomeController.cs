using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using System.Security.Claims;

namespace Pizza.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(IPizzeria pizzeriaService)
        {
        }
        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["tasty-cookies"];
            

            if (string.IsNullOrEmpty(token))
            {
                ViewBag.IsAdmin = false;
                return View("~/Views/Home/Home.cshtml");
            }

            var roleClaim = User.Claims.FirstOrDefault(c => 
                c.Type == ClaimTypes.Role);
            var isAdmin = roleClaim != null && 
                         roleClaim.Value == "123e4567-e89b-12d3-a456-426614174000"; // UUID роли Admin

            ViewBag.IsAdmin = isAdmin;
            
            return View("~/Views/Home/Home.cshtml");
        }

        [HttpPost("/logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("tasty-cookies");
            return RedirectToAction("Index", "Login");
        }
        [HttpPost("/Home/SelectPizzeria")]
        public IActionResult SelectPizzeria(int pizzeriaId)
        {
            return RedirectToAction("Menu", "PizzeriaPizza", new { pizzeriaId = pizzeriaId });
        }

    }
}