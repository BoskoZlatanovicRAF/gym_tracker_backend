using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymTracker_backend.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest registerRequest)
    {
        try
        {
            var result = await authService.RegisterAsync(registerRequest);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new{ message = ex.Message});
        }
        
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        try
        {
            var result = await authService.LoginAsync(loginRequest);
            return Ok(new { jwt = result });

        }catch (InvalidOperationException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        
    }
}