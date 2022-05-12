using DbAccess.Models;

namespace OGFoodAPI.RecipeService.Models
{
    public class IngredientWithAmount
    {
        public Ingredient Ingredient { get; set; } = new();
        public double Amount { get; set; }
        public string Unit { get; set; } = "";
    }
}
