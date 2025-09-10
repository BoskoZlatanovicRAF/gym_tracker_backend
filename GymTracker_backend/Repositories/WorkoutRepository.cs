using GymTracker_backend.Data;
using GymTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GymTracker_backend.Repositories;

public class WorkoutRepository(AppDbContext db)
{
    public async Task<List<Workout>> GetVisibleToUserAsync(Guid userId)
    {
        return await db.Workouts
            .Where(w => !w.IsCustom || w.CreatedBy == userId)
            .OrderBy(w => w.IsCustom)
            .ToListAsync();
    }

    public async Task<List<Workout>> GetWorkoutByNameAsync(Guid workoutId)
    {
        return await db.Workouts
            .Where(w => w.Id == workoutId)
            .OrderBy(w => w.IsCustom)
            .ToListAsync();
    }
    
    public async Task<Workout> AddAsync(Workout workout)
    {
        db.Workouts.Add(workout);
        await db.SaveChangesAsync();
        return workout;
    }
    
    public async Task<List<WorkoutExercise>> AddExercisesToWorkoutAsync(
        Guid workoutId, 
        List<WorkoutExercise> exercises, 
        Guid userId)
    {
        var workout = await db.Workouts
            .FirstOrDefaultAsync(w => w.Id == workoutId);
        
        if(workout == null)
            throw new InvalidOperationException("Workout not found.");
        
        if(workout.IsCustom && workout.CreatedBy != userId)
            throw new InvalidOperationException("Not allowed to modify this workout");
        
        db.WorkoutExercises.AddRange(exercises);
        await db.SaveChangesAsync();
        
        return await db.WorkoutExercises
            .Where(we => we.WorkoutId == workoutId)
            .Include(we => we.Exercise)
            .ToListAsync();
    }

    public async Task DeleteAsync(string workoutName, Guid userId)
    {
        var workout = await db.Workouts
            .FirstOrDefaultAsync(w => w.Name == workoutName && w.CreatedBy == userId);        
        
        if (workout == null || workout.CreatedBy != userId)
        {
            throw new InvalidOperationException("Workout not found or not owned by user.");
        }
        
        db.Workouts.Remove(workout);
        await db.SaveChangesAsync();
    }
}