using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GymTracker_backend.Data;
using GymTracker_backend.DTOs;
using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Models;
using GymTracker_backend.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GymTracker_backend.Services;

public interface IAuthService
{ 
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    Task<string> LoginAsync(LoginRequest request);
}

public class AuthService(UserRepository userRepository, IConfiguration configuration) : IAuthService
{
    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            throw new InvalidOperationException("Email already exists");
    
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        await userRepository.AddUserAsync(user);

        return new RegisterResponse
        {
            jwt = GenerateJwtToken(user),
        };
    }

    public async Task<string> LoginAsync(LoginRequest request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new InvalidOperationException("Invalid email or password");
        
        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Secret"] ?? "dev-secret"));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds,
            claims:
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            ]
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
