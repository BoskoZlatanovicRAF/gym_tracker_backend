using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTracker_backend.Models;

[Table("exercise_performances")]
public class ExercisePerformance
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("session_id")]
    public Guid SessionId { get; set; }

    [Column("exercise_name")]
    public string ExerciseName { get; set; } = null!;

    [Column("set_number")]
    public int SetNumber { get; set; }

    [Column("reps")]
    public int Reps { get; set; }

    [Column("weight_kg")]
    public double WeightKg { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation: Session
    [ForeignKey(nameof(SessionId))]
    [InverseProperty(nameof(WorkoutSession.Performances))]
    public WorkoutSession Session { get; set; } = null!;

    // Navigation: Exercise
    [ForeignKey(nameof(ExerciseName))]
    [InverseProperty(nameof(Exercise.PerformanceLogs))]
    public Exercise Exercise { get; set; } = null!;
}
