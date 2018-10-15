using Aguila.Models;
using Aguila.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aguila.Controllers
{
    /// <summary>
    /// This is a Configurations Values API which contains GET, PUT, POST DELETE methods
    /// </summary>
    [Produces("application/json")]
    [Route("api/ConfigurationsValues")]
    public class ConfigurationsValuesController : Controller
    {
        private readonly IConfigurationsValuesService configurationServices = null;

        public ConfigurationsValuesController(IConfigurationsValuesService _configurationServices)
        {
            configurationServices = _configurationServices;
        }

        // GET: api/ConfigurationsValues
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await configurationServices.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/ConfigurationsValues/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await configurationServices.GetByID(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //POST: api/ConfigurationsValues
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConfigurationsValuesDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await configurationServices.Insert(data);
            if (result == true)
            {
                return Ok(Properties.Resources.ResourceManager.GetString("SuccCreatdMessage"));
            }
            return BadRequest();
        }

        // PUT: api/ConfigurationsValues/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody]  ConfigurationsValuesDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != data.Id)
            {
                return BadRequest();
            }
            var result = await configurationServices.Update(id, data);
            if (result == true)
            {
                return Ok(Properties.Resources.ResourceManager.GetString("SuccUpdateMessage"));
            }
            return BadRequest();
        }

        // DELETE: api/ConfigurationsValues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool result = await configurationServices.Delete(id);
            if (result == true)
            {
                return Ok(Properties.Resources.ResourceManager.GetString("SuccDeleteMessage"));
            }
            return NotFound();
        }
    }
}