using GymTracker_backend.Data;
using GymTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GymTracker_backend.Repositories;

public class ExercisePerformanceRepository(AppDbContext db)
{
    public async Task RecordManyAsync(List<ExercisePerformance> performances)
    {
        db.ExercisePerformances.AddRange(performances);
        await db.SaveChangesAsync();
    }
    
    public async Task<List<ExercisePerformance>> GetBySessionIdAsync(Guid sessionId)
    {
        return await db.ExercisePerformances
            .Where(p => p.SessionId == sessionId)
            .OrderBy(p => p.SetNumber)
            .ToListAsync();
    }
}
