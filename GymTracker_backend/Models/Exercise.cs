using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTracker_backend.Models;

public class Exercise
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    [Column(TypeName = "text[]")]
    public string[] MuscleGroupNames { get; set; } = [];
    public double MetValue { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsCustom { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Category Category { get; set; } = null!;
    public User? Creator { get; set; }
    public List<WorkoutExercise> WorkoutExercises { get; set; } = [];
    public List<ExercisePerformance> PerformanceLogs { get; set; } = [];
}
