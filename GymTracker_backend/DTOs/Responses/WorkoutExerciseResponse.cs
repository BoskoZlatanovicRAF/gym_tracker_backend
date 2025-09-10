namespace GymTracker_backend.DTOs.Responses;

public class WorkoutExerciseResponse
{
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; } = null!;
    public int OrderInWorkout { get; set; }
    public int TargetSets { get; set; }
    public int TargetReps { get; set; }
    public double? TargetWeightKg { get; set; }
}
