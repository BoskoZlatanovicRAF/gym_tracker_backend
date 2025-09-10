using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GymTracker_backend.Models;

public class Category
{
    [Key]
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation
    public List<Exercise> Exercises { get; set; } = [];
    public List<MuscleGroup> MuscleGroups { get; set; } = [];
}