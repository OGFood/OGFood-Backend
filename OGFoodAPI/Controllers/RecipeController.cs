// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OGFoodAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using static DbAccess.Factory;
    using DbAccess.Interfaces;
    using DbAccess.Models;
    using Microsoft.AspNetCore.Cors;

    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        readonly IRecipeCrud recipes;
        public RecipeController() => recipes = GetRecipeCrud();
        // GET: api/<RecipeController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> Get() => await recipes.GetAllRecipes();

        // GET api/<RecipeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RecipeController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(Recipe recipe)
        {
            await recipes.AddRecipeAsync(recipe);
            return CreatedAtAction(nameof(Get), new { id = recipe.Id }, recipe);
        }

        // PUT api/<RecipeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RecipeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
