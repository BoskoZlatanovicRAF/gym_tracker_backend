namespace GymTracker_backend.DTOs.Requests;

using System.ComponentModel.DataAnnotations;

public class WorkoutExerciseRequest
{
    [Required]
    [MaxLength(100, ErrorMessage = "Exercise name cannot exceed 100 characters.")]
    public string ExerciseName { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "OrderInWorkout must be at least 1.")]
    public int OrderInWorkout { get; set; }

    [Range(1, 100, ErrorMessage = "TargetSets must be at least 1.")]
    public int TargetSets { get; set; }

    [Range(1, 100, ErrorMessage = "TargetReps must be at least 1.")]
    public int TargetReps { get; set; }

    [Range(0, 1000, ErrorMessage = "TargetWeightKg must be non-negative.")]
    public double? TargetWeightKg { get; set; }
}
