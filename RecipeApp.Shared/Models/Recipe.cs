namespace RecipeApp.Shared.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public int PrepTimeMinutes { get; set; }
    public int CookTimeMinutes { get; set; }
    public int Servings { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? ImagePath { get; set; }
    public DateTime CreatedDate { get; set; }
    
    public List<Ingredient> Ingredients { get; set; } = new();
    public NutritionInfo? NutritionInfo { get; set; }
    public List<Rating> Ratings { get; set; } = new();
    public List<RecipeTag> RecipeTags { get; set; } = new();
}

public enum DifficultyLevel
{
    Easy = 1,
    Medium = 2,
    Hard = 3
}
