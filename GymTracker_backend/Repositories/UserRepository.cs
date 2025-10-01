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
        return await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }
    
    public async Task<string?> GetUserNameByIdAsync(Guid? userId)
    {
        var user = await db.Users.FindAsync(userId);
        return user?.FirstName+" "+user?.LastName;
    }
    
    public async Task UpdateUserAsync(User user)
    {
        db.Users.Update(user);
        await db.SaveChangesAsync();
    }
}