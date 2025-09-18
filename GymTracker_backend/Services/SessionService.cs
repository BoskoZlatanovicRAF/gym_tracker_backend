using GymTracker_backend.Data;
using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Mappers;
using GymTracker_backend.Models;
using GymTracker_backend.Repositories;

namespace GymTracker_backend.Services;

public interface ISessionService
{
    Task<WorkoutSessionResponse> StartSessionAsync(Guid userId, StartSessionRequest request);
    Task EndSessionAsync(Guid userId, EndSessionRequest request);
    Task<List<WorkoutSessionResponse>> GetSessionsForUserAsync(Guid userId);
    Task<(string WorkoutName, TimeSpan Duration, double? TotalCalories)> GetLastSessionAsync(Guid userId);
    Task<(string WorkoutName, int Count)> GetMostRepeatedSessionAsync(Guid userId);
    Task<(string WorkoutName, DateTime Date, double TotalCalories)> GetBestWorkoutAsync(Guid userId);
    Task<Dictionary<DayOfWeek, bool>> GetWorkoutsForCurrentWeekAsync(Guid userId);
    Task<List<(DateTime StartDate, DateTime EndDate, int WorkoutCount)>> GetWorkoutsPerWeekInPastMonthAsync(Guid userId);
}

public class SessionService(SessionRepository repo, AppDbContext db) : ISessionService
{
    public async Task<WorkoutSessionResponse> StartSessionAsync(Guid userId, StartSessionRequest request)
    {
        var workout = await db.Workouts.FindAsync(request.WorkoutId);
        if(workout == null)
            throw new Exception("Workout not found");
        
        var session = new WorkoutSession
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            WorkoutId = request.WorkoutId,
            StartTime = DateTime.UtcNow,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await repo.StartAsync(session);
        return created.ToResponse(workout.Name);
    }

    public async Task EndSessionAsync(Guid userId, EndSessionRequest request)
    {
        await repo.EndAsync(userId, DateTime.UtcNow, request.TotalCalories, request.Notes);
    }

    public async Task<List<WorkoutSessionResponse>> GetSessionsForUserAsync(Guid userId)
    {
        var sessions = await repo.GetAllByUserAsync(userId);
        return sessions.Select(s => s.ToResponse(s.Workout?.Name ?? "")).ToList();
    }
    
    public async Task<(string WorkoutName, int Count)> GetMostRepeatedSessionAsync(Guid userId)
    {
        return await repo.GetMostRepeatedSessionAsync(userId);
    }
    
    public async Task<(string WorkoutName, DateTime Date, double TotalCalories)> GetBestWorkoutAsync(Guid userId)
    {
        return await repo.GetBestWorkoutAsync(userId);
    }
    
    public async Task<Dictionary<DayOfWeek, bool>> GetWorkoutsForCurrentWeekAsync(Guid userId)
    {
        return await repo.GetWorkoutsForCurrentWeekAsync(userId);
    }
    
    public async Task<List<(DateTime StartDate, DateTime EndDate, int WorkoutCount)>> GetWorkoutsPerWeekInPastMonthAsync(Guid userId)
    {
        return await repo.GetWorkoutsPerWeekInPastMonthAsync(userId);
    }
    
    public async Task<(string WorkoutName, TimeSpan Duration, double? TotalCalories)> GetLastSessionAsync(Guid userId)
    {
        return await repo.GetLastSessionAsync(userId);
    }
    
}
