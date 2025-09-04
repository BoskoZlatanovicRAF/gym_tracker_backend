using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymTracker_backend.Models;

[Table("muscle_groups")]
public class MuscleGroup
{
    [Key] [Column("name")] 
    public string Name { get; set; } = null!;
    
    [Column("category_name")]
    public string CategoryName { get; set; } = null!;
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Category Category { get; set; }
}