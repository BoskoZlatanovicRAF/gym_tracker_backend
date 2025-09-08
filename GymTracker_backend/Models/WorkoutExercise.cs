using System.ComponentModel.DataAnnotations.Schema;

namespace GymTracker_backend.Models;

[Table("workout_exercises")]
public class WorkoutExercise
{
    [Column("workout_name")]
    public string WorkoutName { get; set; } = null!;

    [Column("exercise_name")]
    public string ExerciseName { get; set; } = null!;

    [Column("order_in_workout")]
    public int OrderInWorkout { get; set; }

    [Column("target_sets")]
    public int TargetSets { get; set; }

    [Column("target_reps")]
    public int TargetReps { get; set; }

    [Column("target_weight_kg")]
    public double? TargetWeightKg { get; set; }

    // Navigation
    public Workout Workout { get; set; } = null!;
    public Exercise Exercise { get; set; } = null!;
}
