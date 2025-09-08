using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Helpers;
using GymTracker_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymTracker_backend.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/sessions")]
public class SessionController(ISessionService service) : ControllerBase
{
    [HttpPost("start")]
    public async Task<ActionResult<WorkoutSessionResponse>> Start([FromBody] StartSessionRequest request)
    {
        var userId = User.GetUserId();
        var result = await service.StartSessionAsync(userId, request);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPatch("{id}/end")]
    public async Task<IActionResult> End(Guid id, [FromBody] EndSessionRequest request)
    {
        await service.EndSessionAsync(id, request);
        return Ok(new { message = "Session ended" });
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkoutSessionResponse>>> GetAll()
    {
        var userId = User.GetUserId();
        var result = await service.GetSessionsForUserAsync(userId);
        return Ok(new { sessions = result });
    }
}
