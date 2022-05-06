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
        public async Task<IEnumerable<Ingredient>> Get()
        {
            return await ingredients.GetAllIngredients();
        }

        // GET api/<IngredientsController>/5
        [HttpGet("{id}")]
        public async Task<Ingredient> Get(string id)
        {
            return await ingredients.GetIngredientById(id);
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
