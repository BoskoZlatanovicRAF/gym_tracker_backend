namespace GymTracker_backend.DTOs.Responses;

public class WorkoutDetailsResponse
{
    public string Name { get; set; } = null!;
    public string? CreatedByName { get; set; }
    public string Description { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public int SessionCount { get; set; }
}