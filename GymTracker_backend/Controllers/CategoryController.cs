using GymTracker_backend.Models;
using GymTracker_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymTracker_backend.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<Category>>> GetAllCategories()
    {
        var result = await categoryService.GetAllCategoriesAsync();
        
        return Ok(new {categories = result});
    }
}