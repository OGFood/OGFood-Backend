using SharedInterfaces.Models;

namespace DbAccess.Interfaces
{

    public interface IIngredientCrud
    {

        public Task<Ingredient> GetIngredientByName(string name);
        public Task<Ingredient> GetIngredientById(string id);
        public Task<List<Ingredient>> GetIngredientsByNameBeginsWith(string searchString);
        public Task<List<Ingredient>> GetAllIngredients();
        public Task AddIngredientAsync(Ingredient ingredient);
        public Task UpdateIngredientAsync(string id, Ingredient ingredient);
        public Task DeleteIngredientAsync(string id);
    }
}
