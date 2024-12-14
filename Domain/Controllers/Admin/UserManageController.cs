using Core.Dto;
using Microsoft.AspNetCore.Mvc;
using Core.Dto.Pizza;
using Core.Interfaces;
using Core.Models;

namespace Pizza.Controllers.Admin
{
    public class UserManageController : Controller
    {
        private readonly IUser _userService;

        public UserManageController(IUser userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("Admin/ManageUser")]
        public async Task<IActionResult> ManageUser()
        {
            var users = await _userService.GetAllUsers();
            return View("~/Views/Admin/User/ManageUser.cshtml", users);
        }

        [HttpGet]
        [Route("Admin/EditUser/{id}")]
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UpdateUserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                SurName = user.SurName,
                Email = user.Email,
                Phone = user.Phone
            };

            return View("~/Views/Admin/User/EditUser.cshtml", userDto);
        }

        [HttpPost]
        [Route("Admin/UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto userDto, int id)
        {
            if (ModelState.IsValid)
            {
                var updatedUser = await _userService.UpdateUser(userDto, id);
                if (updatedUser != null)
                {
                    return RedirectToAction("ManageUser");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update pizza.");
                }
            }
            return View("~/Views/Admin/User/EditUser.cshtml", userDto);
        }

        [HttpGet]
        [Route("Admin/AddUser")]
        public IActionResult AddUser()
        {
            var userDto = new PizzaDto();  
            return View("~/Views/Admin/User/AddUser.cshtml"); 
        }

        [HttpPost]
        [Route("Admin/AddUser")]
        public async Task<IActionResult> AddUser(Users users)
        {
            if (ModelState.IsValid)
            {
                var newUser = await _userService.CreateUser(users);
                if (newUser != null)
                {
                    return RedirectToAction("ManageUser");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to add pizza.");
                }
            }
            return View("~/Views/Admin/User/AddUser.cshtml");
        }

        [HttpPost]
        [Route("Admin/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userService.DeleteUser(id);
            if (success)
            {
                return RedirectToAction("ManageUser");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete pizza.");
                return RedirectToAction("ManageUser");
            }
        }
    }
}
