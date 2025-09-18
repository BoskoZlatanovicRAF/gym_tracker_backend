using GymTracker_backend.Data;
using GymTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GymTracker_backend.Repositories;



public class UserRepository(AppDbContext db)
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddUserAsync(User user)
    {
        db.Users.Add(user);
        await db.SaveChangesAsync();
    }
    
    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await db.Users
            .Where(u => u.Id == userId)
            .Select(u => new User
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                HeightCm = u.HeightCm,
                WeightKg = u.WeightKg,
                PreferredUnits = u.PreferredUnits
            })
            .FirstOrDefaultAsync();
    }
}