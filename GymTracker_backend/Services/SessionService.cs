using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Mappers;
using GymTracker_backend.Models;
using GymTracker_backend.Repositories;

namespace GymTracker_backend.Services;

public interface ISessionService
{
    Task<WorkoutSessionResponse> StartSessionAsync(Guid userId, StartSessionRequest request);
    Task EndSessionAsync(Guid sessionId, EndSessionRequest request);
    Task<List<WorkoutSessionResponse>> GetSessionsForUserAsync(Guid userId);
}

public class SessionService(SessionRepository repo) : ISessionService
{
    public async Task<WorkoutSessionResponse> StartSessionAsync(Guid userId, StartSessionRequest request)
    {
        var session = new WorkoutSession
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            WorkoutName = request.WorkoutName,
            StartTime = DateTime.UtcNow,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await repo.StartAsync(session);
        return created.ToResponse();
    }

    public async Task EndSessionAsync(Guid sessionId, EndSessionRequest request)
    {
        await repo.EndAsync(sessionId, DateTime.UtcNow, request.TotalCalories, request.Notes);
    }

    public async Task<List<WorkoutSessionResponse>> GetSessionsForUserAsync(Guid userId)
    {
        var sessions = await repo.GetAllByUserAsync(userId);
        return sessions.Select(s => s.ToResponse()).ToList();
    }
}
