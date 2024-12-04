using Core.Dto;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Content.Services;
using Microsoft.AspNetCore.Mvc;

namespace Pizza.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffController:ControllerBase
{
    private readonly IStaff _staffService;

    public StaffController(StaffService staffService)
    {
        _staffService = staffService;
    }

    [HttpGet]
    [Route("/staff")]
    public async Task<IActionResult> GetStaff(int id)
    {
        var staff = await _staffService.GetStaffById(id);
        return Ok(staff);
    }
    
    [HttpPost]
    public async Task<ActionResult> AddStaff([FromBody] StaffDto staff)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var addedStaff = await _staffService.AddStaff(staff);
        return Ok(addedStaff);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStaff(int id, [FromBody] UpdateStaffDto staff)
    {

        var updatedStaff = await _staffService.UpdateStaff(staff, id);

        if (updatedStaff == null)
        {
            return NotFound($"Staff member with ID {id} not found.");
        }

        return Ok(updatedStaff);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStaff(int id)
    {
        var result = await _staffService.DeleteStaff(id);

        if (!result)
        {
            return NotFound($"Staff member with ID {id} not found.");
        }

        return NoContent();
    }
}