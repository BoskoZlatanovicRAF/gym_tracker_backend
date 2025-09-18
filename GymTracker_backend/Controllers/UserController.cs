using GymTracker_backend.Helpers;
using GymTracker_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymTracker_backend.Controllers;


[Authorize]
[ApiController]
[Route("api/v1/users")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpGet("profile")]
    public async Task<ActionResult> GetUserProfile()
    {
        var userId = User.GetUserId();
        var user = await service.GetUserDataAsync(userId);

        if (user == null)
            return NotFound(new { message = "User not found" });

        return Ok(new
        {
            user.Email,
            user.FirstName,
            user.LastName,
            user.HeightCm,
            user.WeightKg,
            user.PreferredUnits
        });
    }
}