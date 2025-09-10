namespace GymTracker_backend.DTOs.Responses;

public class ExerciseResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string[] MuscleGroupNames { get; set; } = null!;
    public double MetValue { get; set; }
    public string? Description { get; set; } = null!;
    public string? ImageUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}