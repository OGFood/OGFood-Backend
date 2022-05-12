namespace OGFoodAPI.RecipeService.Models
{
    public class Recipe
    {
        public string Name = "";
        public string Description = "";
        public List<IngredientWithAmount> IngredientsWithAmount { get; set; } = new();
    }
}
