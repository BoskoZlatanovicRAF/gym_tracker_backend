using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.Helpers;
using GymTracker_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymTracker_backend.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/sessions/performance")]
public class ExercisePerformanceController(IExercisePerformanceService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Record([FromBody] List<ExercisePerformanceRequest> requests)
    {
        var userId = User.GetUserId();
        await service.RecordForActiveSessionAsync(userId, requests);
        return Created();
    }
}
