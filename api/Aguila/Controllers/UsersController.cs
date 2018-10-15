using Aguila.Models;
using Aguila.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aguila.Controllers
{
    /// <summary>
    /// This class contains methods to get user details, delete and register user,
    /// and change user details.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator, User")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// This action method can be used to get particular user details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Users/5
        [Route("GetUsers/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            UserDto result =await _userService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// This action method register the user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/User/PostUsers
        [HttpPost]
        [Route("PostUsers")]
        [AllowAnonymous]
        public async Task<IActionResult> PostUsers([FromBody] ApplicationUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            bool result = await _userService.Insert(model);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();

        }

        /// <summary>
        /// This action method register the user by App
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: api/User/NewUser
        [HttpPost]
        [Route("NewUser")]
        [AllowAnonymous]
        public async Task<IActionResult> NewUser(RegisterAppUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

           var result = await _userService.RegisterByApp(model);
            if (result==null)
            {
                return BadRequest();
            }
            return Ok(result);

        }

        /// <summary>
        /// This action method updates the user details, if already registered.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        // PUT: api/User/5
        [Route("PutUsers/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers([FromRoute] string id, [FromBody] ApplicationUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            var result = await _userService.Update(id, model);
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// This action method deletes the user account from the application.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Users/5
        [Authorize(Roles = "Administrator")]
        [Route("DeleteUsers/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(ModelState);
            }

            ApplicationUserDto result = await _userService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

