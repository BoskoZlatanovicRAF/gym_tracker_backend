using System.ComponentModel.DataAnnotations.Schema;

namespace GymTracker_backend.Models;

[Table("workout_sessions")]
public class WorkoutSession
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("workout_name")]
    public string? WorkoutName { get; set; }

    [Column("start_time")]
    public DateTime StartTime { get; set; }

    [Column("end_time")]
    public DateTime? EndTime { get; set; }

    [Column("total_calories")]
    public double? TotalCalories { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    // Navigation
    public User User { get; set; }
    public Workout? Workout { get; set; }
    public List<ExercisePerformance> ExercisePerformances { get; set; }
}
