using System.ComponentModel.DataAnnotations;

namespace GymTracker_backend.DTOs.Requests;

public class StartSessionRequest
{
    [Required]
    public Guid WorkoutId{ get; set; }

    [MaxLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
    public string? Notes { get; set; }
}

public class EndSessionRequest
{
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "TotalCalories must be non-negative.")]
    public double TotalCalories { get; set; }

    [MaxLength(1000, ErrorMessage = "Notes cannot exceed 1000 characters.")]
    public string? Notes { get; set; }
}