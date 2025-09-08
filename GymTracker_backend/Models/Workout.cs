using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTracker_backend.Models;

[Table("workouts")]
public class Workout
{
    [Key]
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
    [ForeignKey(nameof(CreatedBy))]
    [InverseProperty(nameof(User.CustomWorkouts))]
    public User? Creator { get; set; }
    
    public List<WorkoutExercise> WorkoutExercises { get; set; } = [];
    
    [InverseProperty(nameof(WorkoutSession.Workout))]
    public List<WorkoutSession> Sessions { get; set; } = [];
}
