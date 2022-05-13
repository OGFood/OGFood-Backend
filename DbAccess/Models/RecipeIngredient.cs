namespace DbAccess.Models
{
using MongoDB.Bson.Serialization.Attributes;

    public class RecipeIngredient
    {   
        public Ingredient Ingredient { get; set; } = new();
        public float Amount { get; set; } = 0;
        public string Unit { get; set; } = "";
    }
}
