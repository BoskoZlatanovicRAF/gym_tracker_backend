using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTracker_backend.Models;

public class User
{

    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int? HeightCm { get; set; }
    public double? WeightKg { get; set; }
    public string PreferredUnits { get; set; } = "metric";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
    
    // Navigation
    public List<WorkoutSession> WorkoutSessions { get; set; } = [];
    public List<Exercise> CustomExercises { get; set; } = [];
    public List<Workout> CustomWorkouts { get; set; } = [];
}
