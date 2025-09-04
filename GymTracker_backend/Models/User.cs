using System.ComponentModel.DataAnnotations.Schema;

namespace GymTracker_backend.Models;

[Table("users")]
public class User
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("email")]
    public string Email { get; set; } = null!;
    
    [Column("password_hash")]
    public string PasswordHash { get; set; } = null!;
    
    [Column("first_name")]
    public string FirstName { get; set; } = null!;
    
    [Column("last_name")]
    public string LastName { get; set; } = null!;
    
    [Column("height_cm")]
    public int? HeightCm { get; set; }
    
    [Column("weight_kg")]
    public double? WeightKg { get; set; }
    
    [Column("preferred_units")]
    public string PreferredUnits { get; set; } = "metric";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    public List<WorkoutSession> WorkoutSessions { get; set; }
    public List<Exercise> CustomExercises { get; set; }
    public List<Workout> CustomWorkouts { get; set; }
}
