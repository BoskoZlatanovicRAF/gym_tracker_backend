using GymTracker_backend.Models;
using GymTracker_backend.Repositories;

namespace GymTracker_backend.Services;

public interface IUserService
{
    Task<User?> GetUserDataAsync(Guid userId);
}


public class UserService(UserRepository repo) : IUserService
{
    public async Task<User?> GetUserDataAsync(Guid userId)
    {
        return await repo.GetUserByIdAsync(userId);
    }
}