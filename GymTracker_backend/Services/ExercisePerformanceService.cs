using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Mappers;
using GymTracker_backend.Models;
using GymTracker_backend.Repositories;

namespace GymTracker_backend.Services;

public interface IExercisePerformanceService
{
    Task RecordAsync(Guid sessionId, List<ExercisePerformanceRequest> requests);
    Task<List<ExercisePerformanceResponse>> GetBySessionIdAsync(Guid sessionId);

}

public class ExercisePerformanceService(ExercisePerformanceRepository repo) : IExercisePerformanceService
{
    public async Task RecordAsync(Guid sessionId, List<ExercisePerformanceRequest> requests)
    {
        var performances = requests.Select(r => new ExercisePerformance
        {
            SessionId = sessionId,
            ExerciseId = r.ExerciseId,
            SetNumber = r.SetNumber,
            Reps = r.Reps,
            WeightKg = r.WeightKg,
            CreatedAt = DateTime.UtcNow
        }).ToList();

        await repo.RecordManyAsync(performances);
    }

    public async Task<List<ExercisePerformanceResponse>> GetBySessionIdAsync(Guid sessionId)
    {
        var performances = await repo.GetBySessionIdAsync(sessionId);
        return performances.Select(p => p.ToResponse()).ToList();    
    }
}
