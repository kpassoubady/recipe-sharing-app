namespace RecipeApp.Shared.Models;

public class Rating
{
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public int Score { get; set; }
    public string? Review { get; set; }
    public string ReviewerName { get; set; } = string.Empty;
    public DateTime ReviewDate { get; set; }
    
    public Recipe? Recipe { get; set; }
}
