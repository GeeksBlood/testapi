using Aguila.Models;
using Aguila.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aguila.Controllers
{
    /// <summary>
    /// This is an Attributes API which contains GET, PUT, POST DELETE methods
    /// </summary>
    [Produces("application/json")]
    [Route("api/Attributes")]
    [Authorize(Roles = "Administrator, User")]
    public class AttributesController : Controller
    {
        private readonly IAttributeService _attributeServices = null;
        public AttributesController(IAttributeService attributeServices)
        {
            this._attributeServices = attributeServices;
        }

        // GET: api/Attribute
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _attributeServices.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/Attribute/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _attributeServices.GetByID(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/Attribute/GetAttributesByCategory/5
        [Route("GetAttributesByCategory/{Categoryid}")]
        [HttpGet]
        public async Task<IActionResult> GetAttributesByCategory([FromRoute] int Categoryid)
        {
            var result = await _attributeServices.GetAttributesByCategory(Categoryid);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //POST: api/Attribute
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AttributesDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _attributeServices.Insert(data);
            if (result == true)
            {
                return Ok("Successfully Created.");
            }
            return BadRequest();
        }

        // PUT: api/Attribute/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] AttributesDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != data.Id)
            {
                return BadRequest();
            }
            var result = await _attributeServices.Update(id, data);
            if (result == true)
            {
                return Ok("Successfully Updated.");
            }
            return BadRequest();
        }

        // DELETE: api/Attribute/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool result = await _attributeServices.Delete(id);
            if (result == true)
            {
                return Ok("Successfully Deleted.");
            }
            return NotFound();
        }
    }
}