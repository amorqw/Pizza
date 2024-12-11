using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Pizza.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var token = Request.Cookies["tasty-cookies"];

            if (string.IsNullOrEmpty(token))
            {
                ViewBag.IsAdmin = false;
                return View("~/Views/Home/Home.cshtml");
            }

            var roleClaim = User.Claims.FirstOrDefault(c => 
                c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            var isAdmin = roleClaim != null && 
                          string.Equals(roleClaim.Value, "Admin", StringComparison.OrdinalIgnoreCase);

            ViewBag.IsAdmin = isAdmin;

            return View("~/Views/Home/Home.cshtml");
        }

        [HttpPost("/logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("tasty-cookies");
            return RedirectToAction("Index", "Login");
        }
    }
}