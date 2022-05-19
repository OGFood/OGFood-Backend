// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OGFoodAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Cors;
    using OGFoodAPI.DbService.CrudHelpers;
    using OGFoodAPI.DbService;
    using SharedInterfaces.Models;

    [EnableCors("Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        MongoRecipeCrud _recipes;

        public RecipesController(MongoDbContext dbContext) => _recipes = new MongoRecipeCrud(dbContext);


        /// <summary>
        /// Gets all the recipes.
        /// </summary>
        /// <returns>Collection of all the recipes</returns>
        /// <response code="200">Call ok.</response>
        /// <response code="500">Oops! Can't get the recipes right now.</response>
        // GET: api/<RecipeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> Get() => await _recipes.GetAllRecipes();

        /// <summary>
        /// Gets a single recipe by Id.
        /// </summary>
        /// <remarks>The id is a 24 character long string</remarks>
        /// <returns>The recipe specified by the id.</returns>
        /// <response code="200">Recipe found.</response>
        /// <response code="404">Recipe not found.</response>
        /// <response code="500">Oops! Can't get the recipes right now.</response>
        // GET api/<RecipesController>/5
        [HttpGet("{id:length(24)}", Name ="GetRecipe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Recipe>> Get(string id)
        {
            var output = await _recipes.GetRecipeById(id);
            return output==null ? NotFound() : Ok(output);
        }

        /// <summary>
        /// Add a new recipe to the database.
        /// </summary>
        /// <remarks>Important! post a recipe object without id, db will add Id. Note: the ingredients should have ids, but not the whole recipe object</remarks>
        /// <returns>Created at action result</returns>
        /// <response code="201">Recipe added successfully.</response>
        /// <response code="500">Oops! Can't add the recipe right now.</response>
        // POST api/<RecipeController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(Recipe recipe)
        {
            await _recipes.AddRecipeAsync(recipe);
            return CreatedAtAction(nameof(Get), new { id = recipe.Id }, recipe);
        }

        /// <summary>
        /// Updates an existing recipe in the database.
        /// </summary>
        /// <remarks>Will update the recipe with the given id with content of the supplied recipe object</remarks>
        /// <response code="204">Recipe updated successfully.</response>
        /// <response code="404">Couldn't find an recipe with the given id to update.</response>
        /// <response code="500">Oops! Can't update the recipe right now.</response>
        // PUT api/<RecipesController>/5
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, Recipe updateRecipe)
        {
            var recipe = await _recipes.GetRecipeById(id);
            if (recipe == null) return NotFound();

            updateRecipe.Id = recipe.Id;
            await _recipes.UpdateRecipeAsync(id, updateRecipe);

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing recipe from the database.
        /// </summary>
        /// <remarks>Will delete the recipe with the given id from the database.</remarks>
        /// <response code="204">Recipe deleted successfully.</response>
        /// <response code="404">Couldn't find a recipe with the given id to delete.</response>
        /// <response code="500">Oops! Can't delete the recipe right now.</response>
        // DELETE api/<IngredientsController>/5
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var recipe = await _recipes.GetRecipeById(id);
            if (recipe == null) return NotFound();

            await _recipes.DeleteRecipeAsync(id);
            return NoContent();
        }
    }
}
