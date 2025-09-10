using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymTracker_backend.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/muscle-groups")]
public class MuscleGroupController(IMuscleGroupService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<MuscleGroupResponse>>> GetAll()
    {
        var result = await service.GetAllAsync();
        return Ok(new { muscleGroups = result });
    }

    [HttpPost]
    public async Task<ActionResult<MuscleGroupResponse>> Create([FromBody] MuscleGroupRequest request)
    {
        try
        {
            var result = await service.CreateAsync(request);
            return Ok(new { muscleGroup = result });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new{message = ex.Message});
        }
        
    }
}