// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OGFoodAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using static DbAccess.Factory;
    using DbAccess.Interfaces;
    using DbAccess.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        readonly IIngredientCrud ingredients;
        public IngredientsController() => ingredients = GetIngredientCrud();

        // GET: api/<IngredientsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Ingredient>>> Get() => await ingredients.GetAllIngredients();

        // GET api/<IngredientsController>/5
        [HttpGet("{id:length(24)}", Name ="GetIngredient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Ingredient>> Get(string id)
        {
            var output = await ingredients.GetIngredientById(id);
            return output==null ? NotFound() : Ok(output);
        }

        [HttpGet("{search}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Ingredient>>> Search(string search = "")
        {
            var output = await ingredients.GetIngredientsByNameBeginsWith(search);
            return Ok(output);
        }

        // POST api/<IngredientsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<IngredientsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<IngredientsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
