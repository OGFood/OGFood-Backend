using Microsoft.AspNetCore.Mvc;
using SharedInterfaces.Models;
using static DbAccess.Factory;
using DbAccess.Interfaces;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OGFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserCrud _users;
        public UserController(IUserCrud users) => _users = users;


        // GET api/<UserController>/5
        [HttpGet("{name}/{password}")]
        public async Task<ActionResult<User>> Get(string name, string password)
        {
            return await _users.GetUserByName(name, password);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            var succeeded = _users.CreateUser(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
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
            var recipe = await _users.GetRecipeById(id);
            if (recipe == null) return NotFound();

            updateRecipe.Id = recipe.Id;
            await _users.UpdateRecipeAsync(id, updateRecipe);

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
}
