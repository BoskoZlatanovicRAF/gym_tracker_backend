using GymTracker_backend.Data;
using GymTracker_backend.Models;

namespace GymTracker_backend.Seeders;

public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        if(db.Categories.Any()) return;
        
        // ==========================
        // Categories
        // ==========================
        var chest     = new Category { Name = "Chest" };
        var legs      = new Category { Name = "Legs" };
        var arms      = new Category { Name = "Arms" };
        var shoulders = new Category { Name = "Shoulders" };
        var back      = new Category { Name = "Back" };
        var core      = new Category { Name = "Core" };

        db.Categories.AddRange(chest, legs, arms, shoulders, back, core);
        
        // ==========================
        // Muscle Groups
        // ==========================
        var muscleGroups = new[]
        {
            // Chest
            new MuscleGroup {  Name = "Upper Chest", Category = chest },
            new MuscleGroup {  Name = "Middle Chest", Category = chest },
            new MuscleGroup {  Name = "Lower Chest", Category = chest },

            // Legs
            new MuscleGroup {  Name = "Quads", Category = legs },
            new MuscleGroup {  Name = "Hamstrings", Category = legs },
            new MuscleGroup {  Name = "Glutes", Category = legs },
            new MuscleGroup {  Name = "Calves", Category = legs },

            // Arms
            new MuscleGroup {  Name = "Biceps", Category = arms },
            new MuscleGroup {  Name = "Triceps", Category = arms },
            new MuscleGroup {  Name = "Forearms", Category = arms },

            // Shoulders
            new MuscleGroup {  Name = "Front Delts", Category = shoulders },
            new MuscleGroup {  Name = "Side Delts", Category = shoulders },
            new MuscleGroup {  Name = "Rear Delts", Category = shoulders },

            // Back
            new MuscleGroup {  Name = "Lats", Category = back },
            new MuscleGroup {  Name = "Rhomboids", Category = back },
            new MuscleGroup {  Name = "Traps", Category = back },
            new MuscleGroup {  Name = "Lower Back", Category = back },

            // Core
            new MuscleGroup {  Name = "Abs", Category = core },
            new MuscleGroup {  Name = "Obliques", Category = core }
        };
        db.MuscleGroups.AddRange(muscleGroups);
        
        // ==========================
        // Exercises
        // ==========================
        var exercises = new[]
        {
            // Chest
            new Exercise { Id = Guid.NewGuid(), Name = "Bench Press", Category = chest, MuscleGroupNames = new[] { "Upper Chest", "Middle Chest" }, MetValue = 6.0, Description = "Lie on bench, lower barbell to chest, press up", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Push-ups", Category = chest, MuscleGroupNames = new[] { "Upper Chest", "Middle Chest" }, MetValue = 4.0, Description = "Standard push-up from floor", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Incline Dumbbell Press", Category = chest, MuscleGroupNames = new[] { "Upper Chest" }, MetValue = 5.5, Description = "Incline bench press with dumbbells", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Dips", Category = chest, MuscleGroupNames = new[] { "Lower Chest", "Triceps" }, MetValue = 5.0, Description = "Parallel bar dips", IsCustom = false },

            // Legs
            new Exercise { Id = Guid.NewGuid(), Name = "Squats", Category = legs, MuscleGroupNames = new[] { "Quads", "Glutes" }, MetValue = 5.0, Description = "Stand with feet shoulder-width apart, squat down and up", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Deadlift", Category = legs, MuscleGroupNames = new[] { "Hamstrings", "Glutes", "Lower Back" }, MetValue = 6.0, Description = "Hip hinge movement, lift barbell from floor", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Lunges", Category = legs, MuscleGroupNames = new[] { "Quads", "Glutes" }, MetValue = 4.0, Description = "Step forward into lunge position, alternate legs", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Leg Press", Category = legs, MuscleGroupNames = new[] { "Quads", "Glutes" }, MetValue = 4.5, Description = "Press weight with legs on leg press machine", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Calf Raises", Category = legs, MuscleGroupNames = new[] { "Calves" }, MetValue = 3.0, Description = "Rise up on toes, lower down slowly", IsCustom = false },

            // Arms
            new Exercise { Id = Guid.NewGuid(), Name = "Bicep Curls", Category = arms, MuscleGroupNames = new[] { "Biceps" }, MetValue = 3.0, Description = "Curl dumbbells up towards shoulders", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Triceps Dips", Category = arms, MuscleGroupNames = new[] { "Triceps" }, MetValue = 4.0, Description = "Dip down and push up using triceps", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Hammer Curls", Category = arms, MuscleGroupNames = new[] { "Biceps", "Forearms" }, MetValue = 3.5, Description = "Curl dumbbells with neutral grip", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Tricep Extensions", Category = arms, MuscleGroupNames = new[] { "Triceps" }, MetValue = 3.5, Description = "Extend dumbbells overhead", IsCustom = false },

            // Shoulders
            new Exercise { Id = Guid.NewGuid(), Name = "Shoulder Press", Category = shoulders, MuscleGroupNames = new[] { "Front Delts", "Side Delts" }, MetValue = 4.5, Description = "Press dumbbells overhead", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Lateral Raises", Category = shoulders, MuscleGroupNames = new[] { "Side Delts" }, MetValue = 3.0, Description = "Raise dumbbells to the side", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Front Raises", Category = shoulders, MuscleGroupNames = new[] { "Front Delts" }, MetValue = 3.0, Description = "Raise dumbbells to the front", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Rear Delt Flyes", Category = shoulders, MuscleGroupNames = new[] { "Rear Delts" }, MetValue = 3.5, Description = "Fly dumbbells behind body", IsCustom = false },

            // Back
            new Exercise { Id = Guid.NewGuid(), Name = "Pull-ups", Category = back, MuscleGroupNames = new[] { "Lats", "Rhomboids" }, MetValue = 8.0, Description = "Pull body up to bar", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Bent-over Rows", Category = back, MuscleGroupNames = new[] { "Lats", "Rhomboids", "Traps" }, MetValue = 5.0, Description = "Row barbell towards lower chest", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Lat Pulldowns", Category = back, MuscleGroupNames = new[] { "Lats" }, MetValue = 4.5, Description = "Pull bar down to chest", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "T-Bar Rows", Category = back, MuscleGroupNames = new[] { "Lats", "Rhomboids" }, MetValue = 5.5, Description = "Row T-bar towards chest", IsCustom = false },

            // Core
            new Exercise { Id = Guid.NewGuid(), Name = "Planks", Category = core, MuscleGroupNames = new[] { "Abs", "Obliques" }, MetValue = 3.5, Description = "Hold plank position", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Crunches", Category = core, MuscleGroupNames = new[] { "Abs" }, MetValue = 2.5, Description = "Basic abdominal crunches", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Russian Twists", Category = core, MuscleGroupNames = new[] { "Obliques" }, MetValue = 3.0, Description = "Twist torso side to side", IsCustom = false },
            new Exercise { Id = Guid.NewGuid(), Name = "Mountain Climbers", Category = core, MuscleGroupNames = new[] { "Abs" }, MetValue = 6.0, Description = "Alternate bringing knees to chest in plank", IsCustom = false }
        };
        db.Exercises.AddRange(exercises);
        
        
        // ==========================
        // Workouts (with WorkoutExercises)
        // ==========================
        var pushDay = new Workout { Id = Guid.NewGuid(), Name = "Push Day", Description = "Chest, shoulders, and triceps workout", IsCustom = false };
        pushDay.WorkoutExercises = new List<WorkoutExercise>
        {
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Bench Press"), OrderInWorkout = 1, TargetSets = 3, TargetReps = 8, TargetWeightKg = 60 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Shoulder Press"), OrderInWorkout = 2, TargetSets = 3, TargetReps = 10, TargetWeightKg = 25 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Triceps Dips"), OrderInWorkout = 3, TargetSets = 3, TargetReps = 12 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Lateral Raises"), OrderInWorkout = 4, TargetSets = 3, TargetReps = 15, TargetWeightKg = 10 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Push-ups"), OrderInWorkout = 5, TargetSets = 2, TargetReps = 15 }
        };

        var pullDay = new Workout { Id = Guid.NewGuid(), Name = "Pull Day", Description = "Back and biceps workout", IsCustom = false };
        pullDay.WorkoutExercises = new List<WorkoutExercise>
        {
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Pull-ups"), OrderInWorkout = 1, TargetSets = 3, TargetReps = 6 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Bent-over Rows"), OrderInWorkout = 2, TargetSets = 3, TargetReps = 8, TargetWeightKg = 40 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Lat Pulldowns"), OrderInWorkout = 3, TargetSets = 3, TargetReps = 10, TargetWeightKg = 50 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Bicep Curls"), OrderInWorkout = 4, TargetSets = 3, TargetReps = 12, TargetWeightKg = 15 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Hammer Curls"), OrderInWorkout = 5, TargetSets = 3, TargetReps = 12, TargetWeightKg = 12.5 }
        };

        var legDay = new Workout { Id = Guid.NewGuid(), Name = "Leg Day", Description = "Complete lower body workout", IsCustom = false };
        legDay.WorkoutExercises = new List<WorkoutExercise>
        {
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Squats"), OrderInWorkout = 1, TargetSets = 4, TargetReps = 10, TargetWeightKg = 80 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Deadlift"), OrderInWorkout = 2, TargetSets = 3, TargetReps = 6, TargetWeightKg = 100 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Lunges"), OrderInWorkout = 3, TargetSets = 3, TargetReps = 12, TargetWeightKg = 20 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Leg Press"), OrderInWorkout = 4, TargetSets = 3, TargetReps = 15, TargetWeightKg = 120 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Calf Raises"), OrderInWorkout = 5, TargetSets = 4, TargetReps = 20, TargetWeightKg = 40 }
        };

        var coreBlast = new Workout { Id = Guid.NewGuid(), Name = "Core Blast", Description = "Intense core and abs workout", IsCustom = false };
        coreBlast.WorkoutExercises = new List<WorkoutExercise>
        {
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Planks"), OrderInWorkout = 1, TargetSets = 3, TargetReps = 60 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Crunches"), OrderInWorkout = 2, TargetSets = 3, TargetReps = 20 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Russian Twists"), OrderInWorkout = 3, TargetSets = 3, TargetReps = 30 },
            new WorkoutExercise { Exercise = exercises.First(e => e.Name == "Mountain Climbers"), OrderInWorkout = 4, TargetSets = 3, TargetReps = 20 }
        };

        db.Workouts.AddRange(pushDay, pullDay, legDay, coreBlast);

        db.SaveChanges();
    }
}