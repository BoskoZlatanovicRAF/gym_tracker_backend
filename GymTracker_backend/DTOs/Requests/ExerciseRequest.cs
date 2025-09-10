using System.ComponentModel.DataAnnotations;

namespace GymTracker_backend.DTOs.Requests;

public class ExerciseRequest
{
    [Required]
    [MaxLength(100, ErrorMessage = "Exercise name cannot exceed 100 characters.")]
    public string Name { get; set; } = null!;

    [Required] 
    public string CategoryName { get; set; } = null!;

    [Required]
    [MinLength(1, ErrorMessage = "At least one muscle group is required.")]
    public string[] MuscleGroupNames { get; set; } = [];

    [Required]
    [Range(0.1, 50, ErrorMessage = "MetValue must be greater than 0.")]
    public float MetValue { get; set; }

    [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
    public string? Description { get; set; }

    [Url(ErrorMessage = "ImageUrl must be a valid URL.")]
    public string? ImageUrl { get; set; }
}