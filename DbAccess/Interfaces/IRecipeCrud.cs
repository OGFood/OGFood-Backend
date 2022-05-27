namespace DbAccess.Interfaces
{
    using SharedInterfaces.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRecipeCrud
    {
        Task AddRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(string id);
        Task<List<Recipe>> GetAllRecipes();
        Task<Recipe> GetRecipeById(string id);
        Task<Recipe> GetRecipeByName(string name);
        Task<List<Recipe>> GetRecipeByNameBeginsWith(string searchString);
        Task UpdateRecipeAsync(string id, Recipe recipe);
    }
}