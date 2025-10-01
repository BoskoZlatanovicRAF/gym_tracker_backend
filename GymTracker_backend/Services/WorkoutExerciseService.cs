using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Repositories;

namespace GymTracker_backend.Services;

public interface IWorkoutExerciseService
{
    Task<List<WorkoutExerciseResponse>> GetExercisesForWorkoutAsync(Guid workoutId);
    Task<List<ExerciseMetValueResponse>> GetMetValuesForWorkoutAsync(Guid workoutId);
}


public class WorkoutExerciseService(WorkoutExerciseRepository repo) : IWorkoutExerciseService
{
    public async Task<List<WorkoutExerciseResponse>> GetExercisesForWorkoutAsync(Guid workoutId)
    {
        var exercises = await repo.GetExercisesForWorkoutAsync(workoutId);
        return exercises.Select(e => new WorkoutExerciseResponse
        {
            ExerciseId = e.ExerciseId,
            ExerciseName = e.Exercise.Name,
            OrderInWorkout = e.OrderInWorkout,
            TargetSets = e.TargetSets,
            TargetReps = e.TargetReps,
            TargetWeightKg = e.TargetWeightKg
        }).ToList();
    }
    
    
    public async Task<List<ExerciseMetValueResponse>> GetMetValuesForWorkoutAsync(Guid workoutId)
    {
        var exercises = await repo.GetExercisesForWorkoutAsync(workoutId);
        return exercises.Select(e => new ExerciseMetValueResponse
        {
            ExerciseId = e.ExerciseId,
            ExerciseName = e.Exercise.Name,
            MetValue = e.Exercise.MetValue
        }).ToList();
    }
}