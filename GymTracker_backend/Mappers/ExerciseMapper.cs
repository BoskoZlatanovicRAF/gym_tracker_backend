using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Models;

namespace GymTracker_backend.Mappers;

public static class ExerciseMapper
{
    public static ExerciseResponse ToResponse(this Exercise exercise)
    {
        return new ExerciseResponse
        {
            Name = exercise.Name,
            CategoryName = exercise.CategoryName,
            MuscleGroupNames = exercise.MuscleGroupNames,
            MetValue = exercise.MetValue,
            Description = exercise.Description,
            ImageUrl = exercise.ImageUrl,
            CreatedAt = exercise.CreatedAt
        };
    }
}