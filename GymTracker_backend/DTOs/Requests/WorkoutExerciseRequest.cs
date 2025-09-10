namespace GymTracker_backend.DTOs.Requests;

using System.ComponentModel.DataAnnotations;

public class WorkoutExerciseRequest
{
    [Required]
    public Guid ExerciseId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "OrderInWorkout must be at least 1.")]
    public int OrderInWorkout { get; set; }

    [Range(1, 100, ErrorMessage = "TargetSets must be at least 1.")]
    public int TargetSets { get; set; }

    [Range(1, 100, ErrorMessage = "TargetReps must be at least 1.")]
    public int TargetReps { get; set; }

    [Range(0, 1000, ErrorMessage = "TargetWeightKg must be non-negative.")]
    public double? TargetWeightKg { get; set; }
}
