using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Models;

namespace GymTracker_backend.Mappers;

public static class WorkoutSessionMapper
{
    public static WorkoutSessionResponse ToResponse(this WorkoutSession s, string workoutName = "")
    {
        return new WorkoutSessionResponse
        {
            Id = s.Id,
            UserId = s.UserId,
            WorkoutId = s.WorkoutId,
            WorkoutName = workoutName,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            TotalCalories = s.TotalCalories,
            Notes = s.Notes,
            CreatedAt = s.CreatedAt
        };
    }
}
