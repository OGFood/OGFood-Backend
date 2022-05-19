namespace SharedInterfaces.Interfaces
{
    public interface IIngredientWithAmount
    {
        public IIngredient Ingredient { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
    }
}
