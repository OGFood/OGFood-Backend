using System.Collections.Generic;

namespace SharedInterfaces.Interfaces
{
    public interface IRecipe
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instruction { get; set; }
        public int AproxTime { get; set; }
        public string ImgUrl { get; set; }
        public int Servings { get; set; }
        public IEnumerable<string> Instructions { get; set; }
        public IEnumerable<IIngredientWithAmount> Ingredients { get; set; }
    }
}
