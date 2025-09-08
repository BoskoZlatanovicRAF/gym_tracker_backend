using System.ComponentModel.DataAnnotations;

namespace GymTracker_backend.DTOs.Requests;

public class WorkoutRequest
{
    [Required]
    [MaxLength(100, ErrorMessage = "Workout name cannot exceed 100 characters.")]
    public string Name { get; set; } = null!;

    [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
    public string? Description { get; set; }

    [Url(ErrorMessage = "ImageUrl must be a valid URL.")]
    public string? ImageUrl { get; set; }
}