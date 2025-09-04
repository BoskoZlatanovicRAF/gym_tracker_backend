using GymTracker_backend.Models;

namespace GymTracker_backend.DTOs.Requests;

public class ExerciseRequest
{
    public string Name { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string[] MuscleGroupNames { get; set; } = null!;
    public float MetValue { get; set; }
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}