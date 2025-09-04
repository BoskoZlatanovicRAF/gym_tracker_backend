using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymTracker_backend.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/workouts")]
public class WorkoutController(IWorkoutService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<WorkoutResponse>>> GetAll()
    {
        var userId = Guid.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub));
        var result = await service.GetVisibleToUserAsync(userId);
        return Ok(new { workouts = result });
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<List<WorkoutResponse>>> GetByName([FromRoute] string name)
    {
        var result = await service.GetWorkoutByNameAsync(name);
    }

    [HttpPost]
    public async Task<ActionResult<WorkoutResponse>> Create([FromBody] WorkoutRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub));
        var result = await service.CreateAsync(request, userId);
        return Ok(new { workout = result });
    }
    
    

    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        var userId = Guid.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub));
        await service.DeleteAsync(name, userId);
        return NoContent();
    }
}