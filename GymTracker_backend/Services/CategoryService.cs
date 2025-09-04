using GymTracker_backend.Data;
using GymTracker_backend.Models;
using GymTracker_backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymTracker_backend.Services;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync();
}

public class CategoryService(AppDbContext db) : ICategoryService
{
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        var categories = await db.Categories.ToListAsync();
        return categories;
    }
}