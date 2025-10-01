using GymTracker_backend.Data;
using GymTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GymTracker_backend.Repositories;

public class WorkoutExerciseRepository(AppDbContext db)
{
    
    public async Task<List<Workout>> GetMuscleGroupsForWorkouts(
        Guid userId,
        List<string>? categories = null,
        List<string>? muscleGroups = null)
    {
        var workouts = await db.Workouts
            .Where(w => !w.IsCustom || w.CreatedBy == userId)
            .Include(w => w.WorkoutExercises)
            .ThenInclude(we => we.Exercise)
            .ToListAsync();

        if (categories is { Count: > 0 })
        {
            var normalized = categories.Select(c => c.Trim().ToLower()).ToList();

            workouts = workouts.Where(w =>
            {
                var workoutCats = w.WorkoutExercises
                    .Select(we => we.Exercise.CategoryName.Trim().ToLower())
                    .Distinct()
                    .ToList();

                // must contain all requested categories
                return normalized.All(cat => workoutCats.Contains(cat));
            }).ToList();
        }

        if (muscleGroups is { Count: > 0 })
        {
            var normalized = muscleGroups.Select(mg => mg.Trim().ToLower()).ToList();

            workouts = workouts.Where(w =>
            {
                var workoutMgs = w.WorkoutExercises
                    .SelectMany(we => we.Exercise.MuscleGroupNames)
                    .Select(mg => mg.Trim().ToLower())
                    .Distinct()
                    .ToList();

                // must contain all requested muscle groups
                return normalized.All(mg => workoutMgs.Contains(mg));
            }).ToList();
        }

        return workouts;
    }
    
    public async Task<List<WorkoutExercise>> GetExercisesForWorkoutAsync(Guid workoutId)
    {
        return await db.WorkoutExercises
            .Where(we => we.WorkoutId == workoutId)
            .Include(we => we.Exercise)
            .OrderBy(we => we.OrderInWorkout)
            .ToListAsync();
    }



}