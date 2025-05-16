using Core.Dto;
using Core.Dto.User;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Interfaces.Auth;
using Core.Models;

namespace Pizza.Controllers.Admin
{
    public class UserManageController : Controller
    {
        private readonly IUser _userService;
        private readonly IPasswordHasher _passwordHasher;

        public UserManageController(IUser userService, IPasswordHasher passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
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
        public async Task<IActionResult> EditUser(Guid id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UpdateUserDto
            {
                IdUser = user.IdUser,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Email = user.Email,
                Phone = user.Phone,
                IdRole = user.IdRole
            };

            return View("~/Views/Admin/User/EditUser.cshtml", userDto);
        }

        [HttpPost]
        [Route("Admin/UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto userDto, Guid id)
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
                    ModelState.AddModelError(string.Empty, "Не удалось обновить пользователя.");
                }
            }
            return View("~/Views/Admin/User/EditUser.cshtml", userDto);
        }

        [HttpGet]
        [Route("Admin/AddUser")]
        public IActionResult AddUser()
        {
            return View("~/Views/Admin/User/AddUser.cshtml", new RegisterUserDto()); 
        }

        [HttpPost]
        [Route("Admin/AddUser")]
        public async Task<IActionResult> AddUser(RegisterUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = new Users
                {
                    IdUser = Guid.NewGuid(),
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    MiddleName = userDto.MiddleName,
                    Email = userDto.Email,
                    Password = _passwordHasher.Generate(userDto.Password!),
                    Phone = userDto.Phone,
                    IdRole = userDto.IdRole
                };

                var newUser = await _userService.CreateUser(user);
                if (newUser > 0)
                {
                    return RedirectToAction("ManageUser");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Не удалось добавить пользователя.");
                }
            }
            return View("~/Views/Admin/User/AddUser.cshtml", userDto);
        }

        [HttpPost]
        [Route("Admin/DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var success = await _userService.DeleteUser(id);
            if (success)
            {
                return RedirectToAction("ManageUser");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Не удалось удалить пользователя.");
                return RedirectToAction("ManageUser");
            }
        }
    }
}
