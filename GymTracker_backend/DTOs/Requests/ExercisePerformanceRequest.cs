namespace GymTracker_backend.DTOs.Requests;

public class ExercisePerformanceRequest
{
    public string ExerciseName { get; set; } = null!;
    public int SetNumber { get; set; }
    public int Reps { get; set; }
    public double WeightKg { get; set; }
}
