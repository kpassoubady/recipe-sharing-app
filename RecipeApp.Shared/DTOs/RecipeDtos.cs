using RecipeApp.Shared.Models;

namespace RecipeApp.Shared.DTOs;

public class RecipeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public int PrepTimeMinutes { get; set; }
    public int CookTimeMinutes { get; set; }
    public int TotalTimeMinutes => PrepTimeMinutes + CookTimeMinutes;
    public int Servings { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? ImagePath { get; set; }
    public DateTime CreatedDate { get; set; }
    public double AverageRating { get; set; }
    public int RatingCount { get; set; }
    
    public List<IngredientDto> Ingredients { get; set; } = new();
    public NutritionInfoDto? NutritionInfo { get; set; }
    public List<RatingDto> Ratings { get; set; } = new();
    public List<string> Tags { get; set; } = new();
}

public class IngredientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;
}

public class NutritionInfoDto
{
    public int Calories { get; set; }
    public decimal Protein { get; set; }
    public decimal Carbohydrates { get; set; }
    public decimal Fat { get; set; }
    public decimal Fiber { get; set; }
}

public class RatingDto
{
    public int Id { get; set; }
    public int Score { get; set; }
    public string? Review { get; set; }
    public string ReviewerName { get; set; } = string.Empty;
    public DateTime ReviewDate { get; set; }
}

public class CreateRecipeDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public int PrepTimeMinutes { get; set; }
    public int CookTimeMinutes { get; set; }
    public int Servings { get; set; }
    public DifficultyLevel DifficultyLevel { get; set; }
    public string Category { get; set; } = string.Empty;
    public List<IngredientDto> Ingredients { get; set; } = new();
    public NutritionInfoDto? NutritionInfo { get; set; }
    public List<string> Tags { get; set; } = new();
}

public class CreateRatingDto
{
    public int Score { get; set; }
    public string? Review { get; set; }
    public string ReviewerName { get; set; } = string.Empty;
}
