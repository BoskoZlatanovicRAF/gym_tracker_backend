using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Mappers;
using GymTracker_backend.Models;
using GymTracker_backend.Repositories;

namespace GymTracker_backend.Services;

public interface IExercisePerformanceService
{
    Task RecordForActiveSessionAsync(Guid sessionId, List<ExercisePerformanceRequest> requests);
    Task<List<ExercisePerformanceResponse>> GetBySessionIdAsync(Guid sessionId);

}

public class ExercisePerformanceService(ExercisePerformanceRepository repo, SessionRepository sessionRepo) : IExercisePerformanceService
{
public async Task RecordForActiveSessionAsync(Guid userId, List<ExercisePerformanceRequest> requests)
{
    // Find the active session for the user
    var activeSession = await sessionRepo.GetActiveSessionForUserAsync(userId);
    if (activeSession == null)
        throw new InvalidOperationException("No active session found for the user.");

    // Map requests to ExercisePerformance entities
    var performances = requests.Select(r => new ExercisePerformance
    {
        SessionId = activeSession.Id,
        ExerciseId = r.ExerciseId,
        SetNumber = r.SetNumber,
        Reps = r.Reps,
        WeightKg = r.WeightKg,
        CreatedAt = DateTime.UtcNow
    }).ToList();

    // Save the performances
    await repo.RecordManyAsync(performances);
}
    public async Task<List<ExercisePerformanceResponse>> GetBySessionIdAsync(Guid sessionId)
    {
        var performances = await repo.GetBySessionIdAsync(sessionId);
        return performances.Select(p => p.ToResponse()).ToList();    
    }
}
