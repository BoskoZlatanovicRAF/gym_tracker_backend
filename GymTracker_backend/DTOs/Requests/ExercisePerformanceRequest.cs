using System.ComponentModel.DataAnnotations;

namespace GymTracker_backend.DTOs.Requests;

public class ExercisePerformanceRequest
{
    [Required]
    public string ExerciseName { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "SetNumber must be at least 1.")]
    public int SetNumber { get; set; }

    [Range(1, 1000, ErrorMessage = "Reps must be at least 1.")]
    public int Reps { get; set; }

    [Range(0, 2000, ErrorMessage = "WeightKg must be non-negative.")]
    public double WeightKg { get; set; }
}
