namespace DbAccess.Models
{
    using MongoDB.Bson.Serialization.Attributes;
    using SharedInterfaces.Interfaces;

    public class IngredientWithAmount : IIngredientWithAmount
    {
        public IIngredient Ingredient { get; set; } = new Ingredient();
        public double Amount { get; set; }
        public string Unit { get; set; } = "";
    }
}
