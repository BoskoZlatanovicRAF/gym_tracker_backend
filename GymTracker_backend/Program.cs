using System.Text;
using DotNetEnv;
using GymTracker_backend.Data;
using GymTracker_backend.Repositories;
using GymTracker_backend.Seeders;
using GymTracker_backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GymTracker_backend;

public class Program
{
    public static void Main(string[] args)
    {
        Env.Load();
        
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"] ?? "dev-secret"))
                };
            });
        
        builder.Services.AddScoped<UserRepository>();
        builder.Services.AddScoped<ExerciseRepository>();
        builder.Services.AddScoped<WorkoutRepository>();
        builder.Services.AddScoped<SessionRepository>();
        builder.Services.AddScoped<ExercisePerformanceRepository>();
        builder.Services.AddScoped<WorkoutExerciseRepository>();

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IExerciseService, ExerciseService>();
        builder.Services.AddScoped<IWorkoutService, WorkoutService>();
        builder.Services.AddScoped<ISessionService, SessionService>();
        builder.Services.AddScoped<IExercisePerformanceService, ExercisePerformanceService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IMuscleGroupService, MuscleGroupService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IWorkoutExerciseService, WorkoutExerciseService>();
        
        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
        
        // using (var scope = app.Services.CreateScope())
        // {
        //     var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        //     db.Database.EnsureCreated();
        //     DbSeeder.Seed(db);
        // }
        
        app.Run();
    }
}