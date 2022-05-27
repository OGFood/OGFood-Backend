using MongoDB.Driver;
using Newtonsoft.Json;
using NUnit.Framework;
using OGFoodAPI.DbService;
using OGFoodAPI.RecipeService;
using OGFoodAPI.RecipeService.Strategies;
using SharedInterfaces.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Unit_Testing
{
    public class DbStorageMock : DbStorage
    {
        public DbStorageMock(MongoDbContext dbContext) : base(dbContext)
        {
        }

        protected override void RefreshCache()
        {
            _recipes = LoadData<Recipe>("RecipeMock.json");
            //_ingredients = LoadData<Ingredient>("IngredientMock.json");
        }

        private List<T> LoadData<T>(string file)
        {
            string json = "";

            if (File.Exists(file))
                json = File.ReadAllText(file);
            else
                throw new Exception("Couldn't find " + file);

            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }

    public class RecipeService_DbStorage
    {
      

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Ignore("depends on file RecipeMock.json wich is not in repo")]
        
        public async Task Get()
        {
            var recipeService = new RecipeContext(new DbStorageMock(null));

            Recipe recipe = new()
            {
                Id = "627e660df5145afdf6288d7d"
            };

            var actual = await recipeService.Get(recipe);

            var expected = new Recipe()
            {
                Id = "627e660df5145afdf6288d7d",
                Name = "Pancakes",
                Description = "Pancake - a clear favourite! Simple base recipe for thin pancakes. Throw them in the pan and serve with jam, cream, or why not some ice-cream!",
                Instruction = "",
                AproxTime = 20,
                ImgUrl = "https://cdn-rdb.arla.com/Files/arla-se/3249856695/32b74f1c-1632-4aee-aa3b-8fd449494835.jpg?mode=crop&w=1269&h=715&ak=f525e733&hm=e78d4790",
                Servings = 1,
                Instructions = new[] { "Whisk the flour into the milk until it's a smooth mix. Add the rest of the milk, eggs and some salt.",
                    "Let the mix rest for 10 minutes.",
                    "Heat up some butter in a pan and pour the pancake-mix into the pan."
                },
                Ingredients = new IngredientWithAmount[]
                {
                    new IngredientWithAmount()
                    {
                        Ingredient = new Ingredient()
                        {
                            Id = "627e5819b002b6840de51f24",
                            Name= "Eggs",
                        },
                    Amount = 1,
                    Unit = "pcs"
                    },
                    new IngredientWithAmount()
                    {
                        Ingredient = new Ingredient()
                        {
                            Id = "627e5868b002b6840de51f25",
                            Name= "Milk",
                        },
                    Amount = 2,
                    Unit = "dl"
                    },
                    new IngredientWithAmount()
                    {
                        Ingredient = new Ingredient()
                        {
                            Id = "627e5919b002b6840de51f26",
                            Name = "Wheat flour",
                        },
                    Amount = 1,
                    Unit = "dl"
                    },
                    new IngredientWithAmount()
                    {
                        Ingredient = new Ingredient()
                        {
                            Id = "6283a2b2e55e8000a64dcc97",
                            Name = "butter",
                        },
                    Amount = 1,
                    Unit = "dl"
                    },
                }
            };


            Assert.Pass();
        }

        [Test]
        public void Post()
        {
            Assert.Pass();
        }

        [Test]
        public void Patch()
        {
            Assert.Pass();
        }

        [Test]
        public void Delete()
        {
            Assert.Pass();
        }

    }
}