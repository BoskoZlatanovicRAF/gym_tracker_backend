namespace GymTracker_backend.DTOs.Responses;

public class ExercisePerformanceResponse
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public int SetNumber { get; set; }
    public int Reps { get; set; }
    public double WeightKg { get; set; }
    public DateTime CreatedAt { get; set; }
}
