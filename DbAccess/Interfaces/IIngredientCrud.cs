namespace DbAccess.Interfaces
{
    using DbAccess.Models;

    public interface IIngredientCrud
    {
        public Task<Ingredient> GetIngredientByName(string name);
        public Task<Ingredient> GetIngredientById(string id);
        public Task<List<Ingredient>> GetIngredientsByNameBeginsWith(string searchString);
    }
}
