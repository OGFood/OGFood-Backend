using Microsoft.AspNetCore.Mvc;
using SharedInterfaces.Models;
using static DbAccess.Factory;
using DbAccess.Interfaces;
using Microsoft.AspNetCore.Cors;
using DbAccess.Helpers;

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
            Func<ObjectResult> call = new(() => Ok("User added"));
            var status = await _users.CreateUser(user);

            status.ForEach(x =>
            {
                call = (x.Name, x.Success) switch
                {
                    (UserResult.CompletedSuccessfully, true) => () => Ok("User Added"),
                    (UserResult.ValidName, false) => () => BadRequest("Invalid name"),
                    (UserResult.ValidMail, false) => () => BadRequest("Invalid email"),
                    (UserResult.PwdNotTooShort, false) => () => BadRequest("Password too short"),
                    (UserResult.PwdNotTooLong, false) => () => BadRequest("Password too long"),
                    _ => () => Ok("User Added")
                };
            });

            return call.Invoke();
        }

        // PUT api/<UserController>
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> Put(User user)
        {
            _users.UpdateUser(user);
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
        [HttpDelete("{name}/{password}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public async Task<IActionResult> Delete(string name, string password)
        {
            if (await _users.DeleteUser(name, password))
                Ok();

            return BadRequest();
        }
    }
}
