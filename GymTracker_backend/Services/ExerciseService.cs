using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GymTracker_backend.Data;
using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Mappers;
using GymTracker_backend.Models;
using GymTracker_backend.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace GymTracker_backend.Services;

public interface IExerciseService
{
    Task<List<ExerciseResponse>> GetExercisesVisibleToUserAsync(Guid userId);
    Task<ExerciseResponse> CreateExerciseAsync(ExerciseRequest exerciseRequest, Guid userId);
}
public class ExerciseService(ExerciseRepository exerciseRepository, AppDbContext db) : IExerciseService
{
    
    public async Task<List<ExerciseResponse>> GetExercisesVisibleToUserAsync(Guid userId)
    {
        var exercises = await exerciseRepository.GetExercisesVisibleToUserAsync(userId);
        return exercises.Select(e => e.ToResponse()).ToList();
    }

    public async Task<ExerciseResponse> CreateExerciseAsync(ExerciseRequest exerciseRequest, Guid userId)
    {
        var categoryExists = await db.Categories.AnyAsync(c => c.Name == exerciseRequest.CategoryName);
        if (!categoryExists)
            throw new InvalidOperationException($"Category with name '{exerciseRequest.CategoryName}' doesn't exist.");
        
        var existingMuscleGroups = await db.MuscleGroups
            .Where(mg => exerciseRequest.MuscleGroupNames.Contains(mg.Name) && mg.Category.Name == exerciseRequest.CategoryName)
            .Select(mg => mg.Name)
            .ToListAsync();
        var missingGroups = exerciseRequest.MuscleGroupNames.Except(existingMuscleGroups).ToList();
        if (missingGroups.Any())
            throw new InvalidOperationException($"Invalid muscle groups: {string.Join(", ", missingGroups)}");
            
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
        

        try
        {
            var created = await exerciseRepository.AddExerciseAsync(exercise);
            return created.ToResponse();
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException($"Exercise '{exerciseRequest.Name}' already exists for this user.");
        }
    }

}