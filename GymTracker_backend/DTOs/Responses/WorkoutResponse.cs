namespace GymTracker_backend.DTOs.Responses;

public class WorkoutResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsCustom { get; set; }
    public DateTime CreatedAt { get; set; }
}