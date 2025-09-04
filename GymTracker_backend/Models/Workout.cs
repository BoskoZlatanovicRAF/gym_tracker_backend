using System.ComponentModel.DataAnnotations.Schema;

namespace GymTracker_backend.Models;

[Table("workouts")]
public class Workout
{
    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("image_url")]
    public string? ImageUrl { get; set; }

    [Column("is_custom")]
    public bool IsCustom { get; set; }

    [Column("created_by")]
    public Guid? CreatedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User? Creator { get; set; }
    public List<WorkoutExercise> WorkoutExercises { get; set; } = [];
    public List<WorkoutSession> Sessions { get; set; } = [];
}
