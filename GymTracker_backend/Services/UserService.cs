using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.Models;
using GymTracker_backend.Repositories;

namespace GymTracker_backend.Services;

public interface IUserService
{
    Task<User?> GetUserDataAsync(Guid userId);
    Task UpdateUserAsync(Guid userId, UpdateUserRequest request);
}


public class UserService(UserRepository repo) : IUserService
{
    public async Task<User?> GetUserDataAsync(Guid userId)
    {
        return await repo.GetUserByIdAsync(userId);
    }
    
    public async Task UpdateUserAsync(Guid userId, UpdateUserRequest request)
    {
        var user = await repo.GetUserByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found");

        if (!string.IsNullOrEmpty(request.Email))
            user.Email = request.Email;

        if (!string.IsNullOrEmpty(request.FirstName))
            user.FirstName = request.FirstName;

        if (!string.IsNullOrEmpty(request.LastName))
            user.LastName = request.LastName;

        if (request.HeightCm.HasValue)
            user.HeightCm = request.HeightCm;

        if (request.WeightKg.HasValue)
            user.WeightKg = request.WeightKg;

        if (!string.IsNullOrEmpty(request.PreferredUnits))
            user.PreferredUnits = request.PreferredUnits;

        user.UpdatedAt = DateTime.UtcNow;

        await repo.UpdateUserAsync(user);
    }
}