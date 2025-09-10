using GymTracker_backend.Data;
using GymTracker_backend.DTOs.Requests;
using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Mappers;
using GymTracker_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GymTracker_backend.Services;

public interface IMuscleGroupService
{
    Task<List<MuscleGroupResponse>> GetAllAsync();
    Task<MuscleGroupResponse> CreateAsync(MuscleGroupRequest request);
}

public class MuscleGroupService(AppDbContext db) : IMuscleGroupService
{
    public async Task<List<MuscleGroupResponse>> GetAllAsync()
    {
        var groups = await db.MuscleGroups
            .OrderBy(mg => mg.Name)
            .ToListAsync();

        return groups.Select(mg => mg.ToResponse()).ToList();
    }

    public async Task<MuscleGroupResponse> CreateAsync(MuscleGroupRequest request)
    {
        
        if (await db.MuscleGroups.AnyAsync(mg => mg.Name.ToLower() == request.Name.ToLower()))
            throw new InvalidOperationException($"Muscle group with name '{request.Name}' already exists.");


        if (await db.Categories.AnyAsync(c => c.Name.ToLower() != request.CategoryName.ToLower()))
            throw new InvalidOperationException($"Category with name '{request.CategoryName}' doesn't exist.");
        
        var mg = new MuscleGroup
        {
            Name = request.Name,
            CategoryName = request.CategoryName,
            CreatedAt = DateTime.UtcNow
        };

        db.MuscleGroups.Add(mg);
        await db.SaveChangesAsync();

        return mg.ToResponse();
    }
}
