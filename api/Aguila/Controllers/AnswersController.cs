using Aguila.Models;
using Aguila.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aguila.Controllers
{
    /// <summary>
    /// This is an Answers API which contains GET, PUT, POST DELETE methods
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator, User")]
    public class AnswersController : Controller
    {
        private readonly IAnswerService answerServices = null;

        public AnswersController(IAnswerService _answerServices)
        {
            answerServices = _answerServices;
        }


        // GET: api/Answers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await answerServices.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await answerServices.GetByID(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //POST: api/Answers
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AnswersDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await answerServices.Insert(data);
            if (result == true)
            {
                return Ok(Properties.Resources.ResourceManager.GetString("SuccCreatdMessage"));
            }
            return BadRequest();
        }

        // PUT: api/Answers/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody]  AnswersDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != data.Id)
            {
                return BadRequest();
            }
            var result = await answerServices.Update(id, data);
            if (result == true)
            {
                return Ok(Properties.Resources.ResourceManager.GetString("SuccUpdateMessage"));
            }
            return BadRequest();
        }

        // DELETE: api/Answers/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool result = await answerServices.Delete(id);
            if (result == true)
            {
                return Ok(Properties.Resources.ResourceManager.GetString("SuccDeleteMessage"));
            }
            return NotFound();
        }
    }
}