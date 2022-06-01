﻿using Microsoft.AspNetCore.Mvc;
using SharedInterfaces.Models;
using static DbAccess.Factory;
using DbAccess.Interfaces;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OGFoodAPI.Controllers
{
    [EnableCors("Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserCrud _users;
        public UserController(IUserCrud users) => _users = users;

        // GET api/<UserController>/5
        [HttpGet("allusers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _users.GetAllUsers();
        }

        // GET api/<UserController>/5
        [HttpGet("{name}/{password}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<User>> Get(string name, string password)
        {
            var status = await _users.GetUserByName(name, password);

            return status;
        }

        // POST api/<UserController>
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<bool>> Post(User user)
        {
            var status = await _users.CreateUser(user);
            return status;
        }


        /// <summary>
        /// Deletes an existing recipe from the database.
        /// </summary>
        /// <remarks>Will delete the recipe with the given id from the database.</remarks>
        /// <response code="204">Recipe deleted successfully.</response>
        /// <response code="404">Couldn't find a recipe with the given id to delete.</response>
        /// <response code="500">Oops! Can't delete the recipe right now.</response>
        // DELETE api/<IngredientsController>/5
        [HttpDelete("{name}/{password}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> Delete(string name, string password)
        {
            _users.DeleteUser(name, password);
            return NoContent();
        }
    }
}
