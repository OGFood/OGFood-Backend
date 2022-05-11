namespace OGFoodAPI.RecipeService.Models
{
    public class Recipe
    {
        public string Name = "";
        public string Description = "";
        public List<IngredientWithAmount> IngredientsTable { get; set; }
    }
}
