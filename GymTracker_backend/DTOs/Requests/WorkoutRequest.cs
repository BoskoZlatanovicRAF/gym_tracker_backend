namespace GymTracker_backend.DTOs.Requests;

public class WorkoutRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}