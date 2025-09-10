namespace GymTracker_backend.DTOs.Responses;

public class WorkoutSessionResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkoutId { get; set; }
    public string WorkoutName { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public double? TotalCalories { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
