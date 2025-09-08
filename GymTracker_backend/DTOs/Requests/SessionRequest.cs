namespace GymTracker_backend.DTOs.Requests;

public class StartSessionRequest
{
    public string? WorkoutName { get; set; }
    public string? Notes { get; set; }
}

public class EndSessionRequest
{
    public double? TotalCalories { get; set; }
    public string? Notes { get; set; }
}
