using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Helpers;
using GymTracker_backend.Models;
using GymTracker_backend.Repositories;
using GymTracker_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymTracker_backend.Controllers;
[ApiController]
[Route("api/v1/exercises")]
public class ExerciseController(IExerciseService exerciseService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ExerciseResponse>>> GetAllExercises()
    {
        var userId = User.GetUserId();
        var result = await exerciseService.GetExercisesVisibleToUserAsync(userId);
        return Ok(new { exercises = result });
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ExerciseResponse>> CreateExercise([FromBody] ExerciseRequest exerciseRequest)
    {
        var userId = User.GetUserId();
        var result = await exerciseService.CreateExerciseAsync(exerciseRequest, userId);
        return Ok(new { exercise = result });
    }
}