using GymTracker_backend.Data;
using GymTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GymTracker_backend.Repositories;

public class SessionRepository(AppDbContext db)
{
    public async Task<WorkoutSession> StartAsync(WorkoutSession session)
    {
        db.WorkoutSessions.Add(session);
        await db.SaveChangesAsync();
        return session;
    }

    public async Task EndAsync(Guid userId, DateTime endTime, double? calories, string? notes)
    {
        var session = await db.WorkoutSessions.FirstOrDefaultAsync(s => s.UserId == userId && s.EndTime == null);
        if (session == null)
            throw new InvalidOperationException("No active session found for user.");

        session.EndTime = endTime;
        session.TotalCalories = calories;
        session.Notes = notes;
        session.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
    }

    public async Task<List<WorkoutSession>> GetAllByUserAsync(Guid userId)
    {
        return await db.WorkoutSessions
            .Where(s => s.UserId == userId)
            .Include(s => s.Workout)
            .OrderByDescending(s => s.StartTime)
            .ToListAsync();
    }
}
