namespace GymTracker_backend.DTOs.Requests;

public class UpdateUserRequest
{
    public string? Email { get; set; } = null!;
    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public int? HeightCm { get; set; }
    public double? WeightKg { get; set; }
    public string? PreferredUnits { get; set; } = "metric";
}