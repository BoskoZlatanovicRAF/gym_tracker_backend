using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Mappers;
using GymTracker_backend.Models;
using GymTracker_backend.Repositories;

namespace GymTracker_backend.Services;

public interface IWorkoutService
{
    Task<List<WorkoutResponse>> GetVisibleToUserAsync(Guid userId);
    Task<List<WorkoutResponse>> GetWorkoutByNameAsync(Guid workoutId);
    Task<WorkoutResponse> CreateAsync(WorkoutRequest request, Guid userId);
    Task<List<WorkoutExerciseResponse>> AddExercisesToWorkout(Guid workoutId, List<WorkoutExerciseRequest> exercises, Guid userId);
    Task DeleteAsync(string workoutName, Guid userId);
    Task <List<WorkoutMuscleGroupResponse>> GetMuscleGroupsForWorkouts(Guid userId, List<string> categories, List<string> muscleGroups);
    Task<WorkoutDetailsResponse> GetWorkoutDetailsAsync(Guid workoutId, Guid userId);
}

public class WorkoutService(WorkoutRepository repo, WorkoutExerciseRepository workoutExerciseRepo, UserRepository userRepo, SessionRepository sessionRepo) : IWorkoutService
{
    public async Task<List<WorkoutResponse>> GetVisibleToUserAsync(Guid userId)
    {
        var workouts = await repo.GetVisibleToUserAsync(userId);
        return workouts.Select(w => w.ToResponse()).ToList();
    }

    public async Task<List<WorkoutResponse>> GetWorkoutByNameAsync(Guid workoutId)
    {
        var workouts = await repo.GetWorkoutByNameAsync(workoutId);
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
        Guid workoutId,
        List<WorkoutExerciseRequest> exercises, 
        Guid userId)
    {
        var entities = exercises.Select(r => new WorkoutExercise
        {
            WorkoutId = workoutId,
            ExerciseId = r.ExerciseId,
            OrderInWorkout = r.OrderInWorkout,
            TargetSets = r.TargetSets,
            TargetReps = r.TargetReps,
            TargetWeightKg = r.TargetWeightKg
        }).ToList();
        
        var created = await repo.AddExercisesToWorkoutAsync(workoutId, entities, userId);
        return created.Select(e => e.ToResponse()).ToList();
    }

    public async Task DeleteAsync(string workoutName, Guid userId)
    {
        await repo.DeleteAsync(workoutName, userId);
    }

    public async Task<List<WorkoutMuscleGroupResponse>> GetMuscleGroupsForWorkouts(Guid userId, List<string>? categories, List<string>? muscleGroups)
    {
        var workouts = await workoutExerciseRepo.GetMuscleGroupsForWorkouts(userId, categories, muscleGroups);
        return workouts.Select(w =>
        {
            var muscleGroups = w.WorkoutExercises
                .SelectMany(we => we.Exercise.MuscleGroupNames)
                .Distinct()
                .ToList();

            var categories = w.WorkoutExercises
                .Select(we => we.Exercise.CategoryName)
                .Distinct()
                .ToList();

            return new WorkoutMuscleGroupResponse
            {
                Id = w.Id,
                Name = w.Name,
                Description = w.Description,
                ImageUrl = w.ImageUrl,
                IsCustom = w.IsCustom,
                CreatedAt = w.CreatedAt,
                MuscleGroups = muscleGroups,
                Categories = categories
            };
        }).ToList();
    }
    
    public async Task<WorkoutDetailsResponse> GetWorkoutDetailsAsync(Guid workoutId, Guid userId)
    {
        var workout = await repo.GetByIdAsync(workoutId);
        if (workout == null)
            throw new KeyNotFoundException("Workout not found");

        var createdByName = workout.IsCustom
            ? await userRepo.GetUserNameByIdAsync(workout.CreatedBy)
            : null;

        var sessionCount = await sessionRepo.GetSessionCountForWorkoutAsync(userId, workoutId);

        return new WorkoutDetailsResponse
        {
            Name = workout.Name,
            CreatedByName = createdByName,
            Description = workout.Description,
            ImageUrl = workout.ImageUrl,
            SessionCount = sessionCount
        };
    }
}
