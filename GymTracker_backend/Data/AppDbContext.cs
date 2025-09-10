using GymTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GymTracker_backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<MuscleGroup> MuscleGroups { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
    public DbSet<WorkoutSession> WorkoutSessions { get; set; }
    public DbSet<ExercisePerformance> ExercisePerformances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkoutExercise>()
            .HasKey(we => new { we.WorkoutId, we.ExerciseId });
        
        modelBuilder.Entity<Exercise>()
            .HasIndex(e => new { e.Name, e.CreatedBy })
            .IsUnique();

        modelBuilder.Entity<Workout>()
            .HasIndex(w => new { w.Name, w.CreatedBy })
            .IsUnique();
        
        modelBuilder.Entity<WorkoutSession>()
            .HasOne(ws => ws.Workout)
            .WithMany(w => w.Sessions)
            .HasForeignKey(ws => ws.WorkoutId)
            .OnDelete(DeleteBehavior.SetNull); // keep session even if workout is deleted
        base.OnModelCreating(modelBuilder);

    }
}