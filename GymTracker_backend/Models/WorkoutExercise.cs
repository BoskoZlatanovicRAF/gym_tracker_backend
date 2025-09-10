using System.ComponentModel.DataAnnotations.Schema;

namespace GymTracker_backend.Models;

public class WorkoutExercise
{
    public Guid WorkoutId { get; set; }
    public Guid ExerciseId { get; set; }
    public int OrderInWorkout { get; set; }
    public int TargetSets { get; set; }
    public int TargetReps { get; set; }
    public double? TargetWeightKg { get; set; }

    // Navigation
    public Workout Workout { get; set; } = null!;
    public Exercise Exercise { get; set; } = null!;
}
