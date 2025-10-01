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
        session.TotalCalories = calories ?? 0;
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
    
    public async Task<(string WorkoutName, int Count)> GetMostRepeatedSessionAsync(Guid userId)
    {
        var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);

        var result = await db.WorkoutSessions
            .Where(s => s.UserId == userId && s.StartTime >= oneMonthAgo)
            .GroupBy(s => s.WorkoutId)
            .Select(g => new
            {
                WorkoutId = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(g => g.Count)
            .FirstOrDefaultAsync();

        if (result == null)
            return ("No sessions", 0);

        var workout = await db.Workouts.FindAsync(result.WorkoutId);
        return (workout?.Name ?? "Unknown Workout", result.Count);
    }
    
    public async Task<(string WorkoutName, DateTime Date, double TotalCalories)> GetBestWorkoutAsync(Guid userId)
    {
        var result = await db.WorkoutSessions
            .Where(s => s.UserId == userId && s.TotalCalories != null)
            .OrderByDescending(s => s.TotalCalories)
            .Select(s => new
            {
                WorkoutName = s.Workout!.Name,
                Date = s.StartTime,
                TotalCalories = s.TotalCalories.Value
            })
            .FirstOrDefaultAsync();

        if (result == null)
            return ("No workouts", DateTime.MinValue, 0);

        return (result.WorkoutName, result.Date, result.TotalCalories);
    }
    
    public async Task<Dictionary<DayOfWeek, bool>> GetWorkoutsForCurrentWeekAsync(Guid userId)
    {
        var today = DateTime.UtcNow.Date;

        // Normalize so that Monday = 0, Sunday = 6
        int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;

        var startOfWeek = today.AddDays(-diff);         // Monday of the current week
        var endOfWeek = startOfWeek.AddDays(7);         // next Monday (exclusive)

        var sessions = await db.WorkoutSessions
            .Where(s => s.UserId == userId && s.StartTime >= startOfWeek && s.StartTime < endOfWeek)
            .ToListAsync();

        var result = Enum.GetValues<DayOfWeek>()
            .ToDictionary(day => day, day => sessions.Any(s => s.StartTime.DayOfWeek == day));

        return result;
    }

    
    public async Task<List<(DateTime StartDate, DateTime EndDate, int WorkoutCount)>> GetWorkoutsPerWeekInPastMonthAsync(Guid userId)
    {
        var today = DateTime.UtcNow.Date;
        var startOfCurrentWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday); // Start of the current week (Monday)
        var startDate = startOfCurrentWeek.AddDays(-21); // Start date is 3 weeks before the current week

        var sessions = await db.WorkoutSessions
            .Where(s => s.UserId == userId && s.StartTime.Date >= startDate && s.StartTime.Date <= today)
            .ToListAsync();

        var result = new List<(DateTime StartDate, DateTime EndDate, int WorkoutCount)>();

        for (var startOfWeek = startDate; startOfWeek <= startOfCurrentWeek; startOfWeek = startOfWeek.AddDays(7))
        {
            var endOfWeek = startOfWeek.AddDays(6);
            var count = sessions.Count(s => s.StartTime.Date >= startOfWeek && s.StartTime.Date <= endOfWeek);
            result.Add((startOfWeek, endOfWeek, count));
        }

        return result;
    }
    
    public async Task<(string WorkoutName, TimeSpan Duration, double? TotalCalories)> GetLastSessionAsync(Guid userId)
    {
        var session = await db.WorkoutSessions
            .Where(s => s.UserId == userId && s.EndTime != null)
            .OrderByDescending(s => s.EndTime)
            .Include(s => s.Workout)
            .FirstOrDefaultAsync();

        if (session == null)
            return ("No sessions", TimeSpan.Zero, null);

        var duration = session.EndTime.Value - session.StartTime;
        return (session.Workout?.Name ?? "Unknown Workout", duration, session.TotalCalories);
    }
    
    public async Task<Dictionary<DateTime, double>> GetCaloriesBurnedPerDayInCurrentWeekAsync(Guid userId)
    {
        var today = DateTime.UtcNow.Date;

        // Normalize so Monday = 0 … Sunday = 6
        int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;

        var startOfCurrentWeek = today.AddDays(-diff);     // Monday of this week
        var endOfCurrentWeek = startOfCurrentWeek.AddDays(7); // Next Monday (exclusive)

        var sessions = await db.WorkoutSessions
            .Where(s => s.UserId == userId && s.StartTime.Date >= startOfCurrentWeek && s.StartTime.Date < endOfCurrentWeek)
            .ToListAsync();

        var result = sessions
            .GroupBy(s => s.StartTime.Date)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(s => s.TotalCalories ?? 0)
            );

        // Ensure all days of the week are included
        for (var date = startOfCurrentWeek; date < endOfCurrentWeek; date = date.AddDays(1))
        {
            if (!result.ContainsKey(date))
            {
                result[date] = 0;
            }
        }

        return result.OrderBy(r => r.Key).ToDictionary(r => r.Key, r => r.Value);
    }
    
    
    public async Task<int> GetSessionCountForWorkoutAsync(Guid userId, Guid workoutId)
    {
        return await db.WorkoutSessions
            .CountAsync(ws => ws.UserId == userId && ws.WorkoutId == workoutId);
    }
    
    public async Task<WorkoutSession?> GetActiveSessionForUserAsync(Guid userId)
    {
        return await db.WorkoutSessions
            .FirstOrDefaultAsync(ws => ws.UserId == userId && ws.EndTime == null);
    }

    public async Task<List<WorkoutSession>> GetActiveSessionsForUserAsync(Guid userId)
    {
        return await db.WorkoutSessions
            .Where(ws => ws.UserId == userId && ws.EndTime == null)
            .ToListAsync();
    }
    
    public async Task DeleteManyAsync(List<WorkoutSession> sessions)
    {
        db.WorkoutSessions.RemoveRange(sessions);
        await db.SaveChangesAsync();
    }
}
