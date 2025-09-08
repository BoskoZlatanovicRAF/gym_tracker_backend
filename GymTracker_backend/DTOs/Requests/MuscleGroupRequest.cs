using System.ComponentModel.DataAnnotations;

namespace GymTracker_backend.DTOs.Requests;

public class MuscleGroupRequest
{
    [Required]
    [MaxLength(50, ErrorMessage = "Muscle group name cannot exceed 50 characters.")]
    public string Name { get; set; } = null!;
    
    [Required]
    [MaxLength(50, ErrorMessage = "Category name cannot exceed 50 characters.")]
    public string CategoryName { get; set; } = null!;
}