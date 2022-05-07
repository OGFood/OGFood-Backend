using DbAccess.Models;

namespace OGFoodAPI.ApiCaller.Models
{
    public class IngredientTable
    {
        public Ingredient Ingredient { get; set; }
        public double Amount { get; set; }
    }

    public class Recipe
    {
        public string str = "";
        public List<IngredientTable> IngredientsTable { get; set; }
    }
}
