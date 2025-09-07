using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Mappers;
using GymTracker_backend.Models;
using GymTracker_backend.Repositories;

namespace GymTracker_backend.Services;

public interface IWorkoutService
{
    Task<List<WorkoutResponse>> GetVisibleToUserAsync(Guid userId);
    Task<List<WorkoutResponse>> GetWorkoutByNameAsync(string name);
    Task<WorkoutResponse> CreateAsync(WorkoutRequest request, Guid userId);
    Task<List<WorkoutExerciseResponse>> AddExercisesToWorkout(string workoutName, List<WorkoutExerciseRequest> exercises, Guid userId);
    Task DeleteAsync(string workoutName, Guid userId);
}

public class WorkoutService(WorkoutRepository repo) : IWorkoutService
{
    public async Task<List<WorkoutResponse>> GetVisibleToUserAsync(Guid userId)
    {
        var workouts = await repo.GetVisibleToUserAsync(userId);
        return workouts.Select(w => w.ToResponse()).ToList();
    }

    public async Task<List<WorkoutResponse>> GetWorkoutByNameAsync(string name)
    {
        var workouts = await repo.GetWorkoutByNameAsync(name);
        return workouts.Select(w => w.ToResponse()).ToList();
    }

    public async Task<WorkoutResponse> CreateAsync(WorkoutRequest request, Guid userId)
    {
        var workout = new Workout
        {
            Name = request.Name,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            IsCustom = true,
            CreatedBy = userId,
            CreatedAt = DateTime.UtcNow
        };

        var created = await repo.AddAsync(workout);
        return created.ToResponse();
    }

    public async Task<List<WorkoutExerciseResponse>> AddExercisesToWorkout(
        string workoutName,
        List<WorkoutExerciseRequest> exercises, 
        Guid userId)
    {
        var entities = exercises.Select(r => new WorkoutExercise
        {
            WorkoutName = workoutName,
            ExerciseName = r.ExerciseName,
            OrderInWorkout = r.OrderInWorkout,
            TargetSets = r.TargetSets,
            TargetReps = r.TargetReps,
            TargetWeightKg = r.TargetWeightKg
        }).ToList();
        
        var created = await repo.AddExercisesToWorkoutAsync(workoutName, entities, userId);
        return created.Select(e => e.ToResponse()).ToList();
    }

    public async Task DeleteAsync(string workoutName, Guid userId)
    {
        await repo.DeleteAsync(workoutName, userId);
    }
}
