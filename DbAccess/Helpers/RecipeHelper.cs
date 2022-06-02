namespace DbAccess.Helpers
{
    using DbAccess.Interfaces;
    using SharedInterfaces.Models;
    using System.Linq;
    using System.Threading.Tasks;

    internal class RecipeHelper
    {
        private readonly IIngredientCrud _ingredients;
        public RecipeHelper(IIngredientCrud ingredients)
        {
            _ingredients = ingredients;
        }

        internal async Task<Recipe> FixIngredients(Recipe recipe)
        {
            var ingredientsList = await _ingredients.GetAllIngredients();

            if (recipe.Ingredients.Any())
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    if (ingredientsList.Any(x => x.Name.ToLower() == ingredient.Ingredient.Name.ToLower()!))
                    {
                        ingredient.Ingredient.Id = ingredientsList.Where(x => x.Name.ToLower() == ingredient.Ingredient.Name.ToLower()!).Select(x => x.Id).FirstOrDefault()!;
                    }
                    else
                    {
                        var newIngredient = new Ingredient() { Name = $"{ingredient.Ingredient.Name}" };
                        await _ingredients.AddIngredientAsync(newIngredient);
                        ingredient.Ingredient.Id = newIngredient.Id;
                    }
                }
            }
            return recipe;
        }
    }
}
