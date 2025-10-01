namespace GymTracker_backend.DTOs.Responses;

public class WorkoutMuscleGroupResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsCustom { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<string> MuscleGroups { get; set; } = [];
    public List<string> Categories { get; set; } = [];
}