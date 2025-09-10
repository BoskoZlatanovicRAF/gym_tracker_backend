using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymTracker_backend.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/sessions/{sessionId:guid}/performance")]
public class ExercisePerformanceController(IExercisePerformanceService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Record(
        [FromRoute] Guid sessionId,
        [FromBody] List<ExercisePerformanceRequest> requests)
    {
        await service.RecordAsync(sessionId, requests);
        return Created();
    }
}
