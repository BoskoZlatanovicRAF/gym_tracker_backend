using GymTracker_backend.DTOs.Responses;
using GymTracker_backend.Models;

namespace GymTracker_backend.Mappers;

public static class MuscleGroupMapper
{
    public static MuscleGroupResponse ToResponse(this MuscleGroup mg)
    {
        return new MuscleGroupResponse
        {
            Name = mg.Name,
            CategoryName = mg.CategoryName,
            CreatedAt = mg.CreatedAt
        };
    }
}