using Core.Dto;
using Microsoft.AspNetCore.Mvc;
using Core.Dto.Pizza;
using Core.Interfaces;

namespace Pizza.Controllers.Admin
{
    public class StaffManageController : Controller
    {
        private readonly IStaff _staffService;

        public StaffManageController(IStaff staffService)
        {
            _staffService = staffService;
        }

        [HttpGet]
        [Route("Admin/ManageStaffs")]
        public async Task<IActionResult> ManageStaffs()
        {
            var staffs = await _staffService.GetAllStaff();
            return View("~/Views/Admin/Staff/ManageStaffs.cshtml", staffs);
        }

        [HttpGet]
        [Route("Admin/EditStaff/{id}")]
        public async Task<IActionResult> EditStaff(int id)
        {
            var staff = await _staffService.GetStaffById(id);
            if (staff == null)
            {
                return NotFound();
            }

            var staffDto = new StaffDto()
            {
                StaffId = staff.StaffId,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Position = staff.Position,
                HireDate = staff.HireDate
            };

            return View("~/Views/Admin/Staff/EditStaff.cshtml", staffDto);
        }

        [HttpPost]
        [Route("Admin/UpdateStaff/{id}")]
        public async Task<IActionResult> UpdateStaff(UpdateStaffDto staffDto, int id)
        {
            if (ModelState.IsValid)
            {
                var updateStaffs = await _staffService.UpdateStaff(staffDto, id);
                if (updateStaffs != null)
                {
                    return RedirectToAction("ManageStaffs");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update pizza.");
                }
            }
            return View("~/Views/Admin/Staff/EditStaff.cshtml");
        }

        [HttpGet]
        [Route("Admin/Addstaff")]
        public IActionResult AddPizza()
        {
            var pizzaDto = new PizzaDto();  
            return View("~/Views/Admin/Staff/AddStaff.cshtml"); 
        }

        [HttpPost]
        [Route("Admin/AddStaff")]
        public async Task<IActionResult> AddPizza(StaffDto staffDto)
        {
            if (ModelState.IsValid)
            {
                var newStaff = await _staffService.AddStaff(staffDto);
                if (newStaff != null)
                {
                    return RedirectToAction("ManageStaffs");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to add staff");
                }
            }
            return View("~/Views/Admin/Staff/AddStaff.cshtml");
        }

        [HttpPost]
        [Route("Admin/DeleteStaff/{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            var success = await _staffService.DeleteStaff(id);
            if (success)
            {
                return RedirectToAction("ManageStaffs");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete pizza");
                return RedirectToAction("ManageStaffs");
            }
        }
    }
}
