using Aguila.Models;
using Aguila.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aguila.Controllers
{
    /// <summary>
    /// This is a Handicaps API which contains GET, PUT, POST DELETE methods
    /// </summary>
    [Produces("application/json")]
    [Route("api/Handicaps")]
    [Authorize(Roles = "Administrator, User")]
    public class HandicapsController : Controller
    {
        private readonly IHandicapService _handicapService = null;

        public HandicapsController(IHandicapService handicapService)
        {
            this._handicapService = handicapService;
        }

        // GET: api/Handicaps
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _handicapService.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/Handicaps/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _handicapService.GetByID(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //POST: api/Handicaps
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HandicapsDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _handicapService.Insert(data);
            if (result == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        // PUT: api/Handicaps/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] HandicapsDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != data.Id)
            {
                return BadRequest();
            }
            var result = await _handicapService.Update(id, data);
            if (result == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE: api/Handicaps/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool result = await _handicapService.Delete(id);
            if (result == true)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}