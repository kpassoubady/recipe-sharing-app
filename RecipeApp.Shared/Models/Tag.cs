namespace RecipeApp.Shared.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public List<RecipeTag> RecipeTags { get; set; } = new();
}

public class RecipeTag
{
    public int RecipeId { get; set; }
    public int TagId { get; set; }
    
    public Recipe? Recipe { get; set; }
    public Tag? Tag { get; set; }
}
