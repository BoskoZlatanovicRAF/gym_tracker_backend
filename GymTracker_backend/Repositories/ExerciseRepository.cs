using GymTracker_backend.Data;
using GymTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GymTracker_backend.Repositories;


public class ExerciseRepository(AppDbContext db)
{
    public async Task<List<Exercise>> GetExercisesVisibleToUserAsync(Guid userId)
    {
        return await db.Exercises
            .Where(e => !e.IsCustom || e.CreatedBy == userId)
            .ToListAsync();
    }
    
    public async Task<Exercise> AddExerciseAsync(Exercise exercise)
    {
        db.Exercises.Add(exercise);
        await db.SaveChangesAsync();
        return exercise;
    }
    
}