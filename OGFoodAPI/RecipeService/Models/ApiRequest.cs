namespace SharedInterfaces.Models
{
    public class ApiRequest
    {
        public List<IngredientWithAmount> IngredientsWithAmount { get; set; } = new List<IngredientWithAmount>();
    }
}
