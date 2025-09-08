using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTracker_backend.Models;

[Table("exercises")]
public class Exercise
{
    [Key]
    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("category_name")]
    public string CategoryName { get; set; } = null!;

    [Column("muscle_group_names", TypeName = "text[]")]
    public string[] MuscleGroupNames { get; set; } = [];

    [Column("met_value")]
    public double MetValue { get; set; }

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
    [ForeignKey(nameof(CategoryName))]
    public Category Category { get; set; }
    
    [ForeignKey(nameof(CreatedBy))]
    [InverseProperty(nameof(User.CustomExercises))]
    public User? Creator { get; set; }


    public List<WorkoutExercise> WorkoutExercises { get; set; } = [];
    
    [InverseProperty(nameof(ExercisePerformance.Exercise))]
    public List<ExercisePerformance> PerformanceLogs { get; set; } = [];
}
