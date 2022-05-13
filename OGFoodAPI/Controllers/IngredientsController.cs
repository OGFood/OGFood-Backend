// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OGFoodAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using static DbAccess.Factory;
    using DbAccess.Interfaces;
    using DbAccess.Models;
    using Microsoft.AspNetCore.Cors;

using DbAccess.Database;

    [EnableCors("Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        readonly IIngredientCrud _ingredients;
        public IngredientsController(IIngredientCrud ingredients) => _ingredients = ingredients;

        // GET: api/<IngredientsController>        
        /// <summary>
        /// Gets all the ingredients.
        /// </summary>
        /// <returns>Collection of all the ingredients</returns>
        /// <response code="200">Call ok.</response>
        /// <response code="500">Oops! Can't get the ingredients right now.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[EnableCors("Policy1")]
        public async Task<ActionResult<IEnumerable<Ingredient>>> Get() => await _ingredients.GetAllIngredients();

        /// <summary>
        /// Gets a single ingredient by Id.
        /// </summary>
        /// <remarks>The id is a 24 character long string</remarks>
        /// <returns>The ingredient specified by the id.</returns>
        /// <response code="200">Ingredient found.</response>
        /// <response code="404">Ingredient not found.</response>
        /// <response code="500">Oops! Can't get the ingredients right now.</response>
        // GET api/<IngredientsController>/5
        [HttpGet("{id:length(24)}", Name ="GetIngredient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Ingredient>> Get(string id)
        {
            var output = await _ingredients.GetIngredientById(id);
            return output==null ? NotFound() : Ok(output);
        }

        /// <summary>
        /// Search for all ingredients that begins with xxx...
        /// </summary>
        /// <remarks>Use this for searching for all ingredients that begins with a certain character(s).</remarks>
        /// <returns>Collection of the found(if any) ingredients.</returns>
        /// <response code="200">The ingredient(s) found.</response>
        /// <response code="500">Oops! Can't get the ingredients right now.</response>
        [HttpGet("{search}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Ingredient>>> Search(string search = "")
        {
            var output = await _ingredients.GetIngredientsByNameBeginsWith(search);
            return Ok(output);
        }

        /// <summary>
        /// Add a new ingredient to the database.
        /// </summary>
        /// <remarks>Important! Only post an object with name, db will add Id. Example: {"name": "Paprika"}</remarks>
        /// <returns>Created at action result</returns>
        /// <response code="201">Ingredient added successfully.</response>
        /// <response code="500">Oops! Can't add the ingredients right now.</response>
        //POST api/<IngredientsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(Ingredient ingredient)
        {
            await _ingredients.AddIngredientAsync(ingredient);
            return CreatedAtAction(nameof(Get), new {id=ingredient.Id},ingredient);
        }

        /// <summary>
        /// Updates an existing ingredient in the database.
        /// </summary>
        /// <remarks>Will update the ingredient with the given id with content of the supplied ingredient object</remarks>
        /// <response code="204">Ingredient updated successfully.</response>
        /// <response code="404">Couldn't find an igredient with the given id to update.</response>
        /// <response code="500">Oops! Can't update the ingredient right now.</response>
        // PUT api/<IngredientsController>/5
        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(string id, Ingredient updateIngredient)
        {
            var ingredient = await _ingredients.GetIngredientById(id);
            if (ingredient == null) return NotFound();

            updateIngredient.Id = ingredient.Id;
            await _ingredients.UpdateIngredientAsync(id, updateIngredient);

            return NoContent();
        }

        /// <summary>
        /// Deletes an existing ingredient from the database.
        /// </summary>
        /// <remarks>Will delete the ingredient with the given id from the database.</remarks>
        /// <response code="204">Ingredient deleted successfully.</response>
        /// <response code="404">Couldn't find an igredient with the given id to delete.</response>
        /// <response code="500">Oops! Can't delete the ingredient right now.</response>
        // DELETE api/<IngredientsController>/5
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var ingredient = await _ingredients.GetIngredientById(id);
            if (ingredient == null) return NotFound();

            await _ingredients.DeleteIngredientAsync(id);
            return NoContent();
        }
    }
}
