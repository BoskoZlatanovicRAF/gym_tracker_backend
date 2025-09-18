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

    [HttpPatch("end")]
    public async Task<IActionResult> End([FromBody] EndSessionRequest request)
    {
        var userId = User.GetUserId();
        await service.EndSessionAsync(userId, request);
        return Ok(new { message = "Session ended" });
    }

    [HttpGet]
    public async Task<ActionResult<List<WorkoutSessionResponse>>> GetAll()
    {
        var userId = User.GetUserId();
        var result = await service.GetSessionsForUserAsync(userId);
        return Ok(new { sessions = result });
    }
    
    [HttpGet("last-session")]
    public async Task<ActionResult> GetLastSession()
    {
        var userId = User.GetUserId();
        var (workoutName, duration, totalCalories) = await service.GetLastSessionAsync(userId);
    
        return Ok(new
        {
            workoutName,
            duration = duration.ToString(@"hh\:mm\:ss"),
            totalCalories
        });
    }
    
    [HttpGet("most-repeated")]
    public async Task<ActionResult> GetMostRepeatedSession()
    {
        var userId = User.GetUserId();
        var (workoutName, count) = await service.GetMostRepeatedSessionAsync(userId);
        return Ok(new { workoutName, count });
    }
    
    [HttpGet("best-workout")]
    public async Task<ActionResult> GetBestWorkout()
    {
        var userId = User.GetUserId();
        var (workoutName, date, totalCalories) = await service.GetBestWorkoutAsync(userId);
        return Ok(new { workoutName, date, totalCalories });
    }
    
    [HttpGet("workouts-this-week")]
    public async Task<ActionResult> GetWorkoutsForCurrentWeek()
    {
        var userId = User.GetUserId();
        var result = await service.GetWorkoutsForCurrentWeekAsync(userId);
        return Ok(result);
    }
    
    [HttpGet("workouts-per-week")]
    public async Task<ActionResult> GetWorkoutsPerWeekInPastMonth()
    {
        var userId = User.GetUserId();
        var result = await service.GetWorkoutsPerWeekInPastMonthAsync(userId);
        var response = result.Select(r => new
        {
            startDate = r.StartDate.ToString("yyyy-MM-dd"),
            endDate = r.EndDate.ToString("yyyy-MM-dd"),
            workoutCount = r.WorkoutCount
        });
        return Ok(response);
    }
}
