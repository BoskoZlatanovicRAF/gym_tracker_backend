namespace GymTracker_backend.DTOs.Responses;

public class ExerciseMetValueResponse
{
    public Guid ExerciseId { get; set; }
    public string ExerciseName { get; set; } = null!;
    public double MetValue { get; set; }
}