using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Models;

namespace GymTracker_backend.Mappers;

public static class WorkoutExerciseMapper
{
    public static WorkoutExerciseResponse ToResponse(this WorkoutExercise we)
    {
        return new WorkoutExerciseResponse
        {
            ExerciseName = we.ExerciseName,
            OrderInWorkout = we.OrderInWorkout,
            TargetSets = we.TargetSets,
            TargetReps = we.TargetReps,
            TargetWeightKg = we.TargetWeightKg
        };
    }
}
