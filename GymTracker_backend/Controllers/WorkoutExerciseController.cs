using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymTracker_backend.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/workout-exercises")]
public class WorkoutExercisesController(IWorkoutExerciseService service) : ControllerBase
{
    [HttpGet("{workoutId}")]
    public async Task<ActionResult<List<WorkoutExerciseResponse>>> GetExercisesForWorkout([FromRoute] Guid workoutId)
    {
        var result = await service.GetExercisesForWorkoutAsync(workoutId);
        return Ok(new { workoutExercises = result });
    }
    
    [HttpGet("met-values/{workoutId}")]
    public async Task<ActionResult<List<ExerciseMetValueResponse>>> GetMetValuesForWorkout([FromRoute] Guid workoutId)
    {
        var result = await service.GetMetValuesForWorkoutAsync(workoutId);
        return Ok(new { metValues = result });
    }    
}