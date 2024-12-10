using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Pizza.Controllers
{
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
          
            var token = Request.Cookies["tasty-cookies"];

            if (string.IsNullOrEmpty(token))
            {

                return View("~/Views/Home/Home.cshtml", false); 
            }

            return View("~/Views/Home/Home.cshtml", true); 
        }

        // Выход (POST)
        [HttpPost("/logout")]
        public IActionResult Logout()
        {
            
            Response.Cookies.Delete("tasty-cookies");
            
            return RedirectToAction("Index", "Login");
        }
    }
}