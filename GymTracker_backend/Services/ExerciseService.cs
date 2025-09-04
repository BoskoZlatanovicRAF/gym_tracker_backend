using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Mappers;
using GymTracker_backend.Models;
using GymTracker_backend.Repositories;

namespace GymTracker_backend.Services;

public interface IExerciseService
{
    Task<List<ExerciseResponse>> GetExercisesVisibleToUserAsync(Guid userId);
    Task<ExerciseResponse> CreateExerciseAsync(ExerciseRequest exerciseRequest, Guid userId);
}
public class ExerciseService(ExerciseRepository exerciseRepository) : IExerciseService
{
    public async Task<List<ExerciseResponse>> GetExercisesVisibleToUserAsync(Guid userId)
    {
        var exercises = await exerciseRepository.GetExercisesVisibleToUserAsync(userId);
        return exercises.Select(e => e.ToResponse()).ToList();
    }

    public async Task<ExerciseResponse> CreateExerciseAsync(ExerciseRequest exerciseRequest, Guid userId)
    {
        var exercise = new Exercise
        {
            Name = exerciseRequest.Name,
            CategoryName = exerciseRequest.CategoryName,
            MuscleGroupNames = exerciseRequest.MuscleGroupNames,
            MetValue = exerciseRequest.MetValue,
            Description = exerciseRequest.Description,
            ImageUrl = exerciseRequest.ImageUrl,
            IsCustom = true,
            CreatedBy = userId,
            CreatedAt = DateTime.UtcNow
        };

        var created = await exerciseRepository.AddExerciseAsync(exercise);
        return created.ToResponse();
    }

}