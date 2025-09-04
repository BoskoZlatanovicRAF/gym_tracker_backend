using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GymTracker_backend.Models;

[Table("categories")]
public class Category
{
    [Key]
    [Column("name")]
    public string Name { get; set; } = null!;
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public List<Exercise> Exercises { get; set; } = new();
    public List<MuscleGroup> MuscleGroups { get; set; } = new();
}