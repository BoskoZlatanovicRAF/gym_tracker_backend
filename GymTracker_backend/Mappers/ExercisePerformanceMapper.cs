using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Models;

namespace GymTracker_backend.Mappers;

public static class ExercisePerformanceMapper
{
    public static ExercisePerformanceResponse ToResponse(this ExercisePerformance perf)
    {
        return new ExercisePerformanceResponse
        {
            Id = perf.Id,
            ExerciseName = perf.ExerciseName,
            SetNumber = perf.SetNumber,
            Reps = perf.Reps,
            WeightKg = perf.WeightKg,
            CreatedAt = perf.CreatedAt
        };
    }
}
