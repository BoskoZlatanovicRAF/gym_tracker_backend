using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Models;

namespace GymTracker_backend.Mappers;

public static class WorkoutMapper
{
    public static WorkoutResponse ToResponse(this Workout workout)
    {
        return new WorkoutResponse
        {
            Name = workout.Name,
            Description = workout.Description,
            ImageUrl = workout.ImageUrl,
            IsCustom = workout.IsCustom,
            CreatedAt = workout.CreatedAt
        };
    }
}