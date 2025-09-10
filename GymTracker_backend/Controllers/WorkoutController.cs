using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Helpers;
using GymTracker_backend.Models;
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
        var userId = User.GetUserId();
        var result = await service.GetVisibleToUserAsync(userId);
        return Ok(new { workouts = result });
    }

    [HttpGet("{workoutId}")]
    public async Task<ActionResult<List<WorkoutResponse>>> GetByName([FromRoute] Guid workoutId)
    {
        var result = await service.GetWorkoutByNameAsync(workoutId);
        return Ok(new { workouts = result });
    }

    [HttpPost]
    public async Task<ActionResult<WorkoutResponse>> Create([FromBody] WorkoutRequest request)
    {
        var userId = User.GetUserId();
        var result = await service.CreateAsync(request, userId);
        return Ok(new { workout = result });
    }

    [HttpPost("{workoutId:guid}/exercises")]
    public async Task<ActionResult<List<WorkoutExerciseResponse>>> AddExercisesToWorkout(
        [FromRoute] Guid workoutId, 
        [FromBody] List<WorkoutExerciseRequest> exercises)
    {
        var userId = User.GetUserId();
        var result = await service.AddExercisesToWorkout(workoutId, exercises, userId);
        return Ok(new { workoutExercises = result });
    }
    

    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        var userId = User.GetUserId();
        await service.DeleteAsync(name, userId);
        return NoContent();
    }
    

}

