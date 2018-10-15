using Aguila.Models;
using Aguila.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aguila.Controllers
{
    /// <summary>
    /// Lessons, Rules, Did you know of the game API
    /// This is a LRD API which contains GET, PUT, POST DELETE methods
    /// </summary>
    [Produces("application/json")]
    [Route("api/LRD")]
    [Authorize(Roles = "Administrator, User")]
    public class LRDController : Controller
    {
        private readonly ILRDService _lrdServices = null;
        public LRDController(ILRDService lrdServices)
        {
            this._lrdServices = lrdServices;
        }

        // GET: api/LRD/LRDData
        [Route("LRDData")]
        [HttpGet]
        public async Task<IActionResult> LRDData()
        {
            var result = await _lrdServices.GetLRDData();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/LRD/Lessons
        [Route("Lessons")]
        [HttpGet]
        public async Task<IActionResult> LessonsOfTheGame()
        {
            var result = await _lrdServices.GetLessonsData();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/LRD/Rules
        [Route("Rules")]
        [HttpGet]
        public async Task<IActionResult> RulesOfTheGame()
        {
            var result = await _lrdServices.GetRulesData();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/LRD/DidYouKnow
        [Route("DidYouKnow")]
        [HttpGet]
        public async Task<IActionResult> DidYouKnow()
        {
            var result = await _lrdServices.GetDidData();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/LRD/Category
        [Route("Category")]
        [HttpGet]
        public async Task<IActionResult> Category()
        {
            var result = await _lrdServices.GetLRDCategory();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/LRD/Attributes
        [Route("Attributes")]
        [HttpGet]
        public async Task<IActionResult> Attributes()
        {
            var result = await _lrdServices.GetLRDAttribute();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/LRD/LessonAttributes
        [Route("LessonAttributes")]
        [HttpGet]
        public async Task<IActionResult> LessonAttributes()
        {
            var result = await _lrdServices.GetLRDLessonsAttribute();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/LRD/RuleAttributes
        [Route("RuleAttributes")]
        [HttpGet]
        public async Task<IActionResult> RuleAttributes()
        {
            var result = await _lrdServices.GetLRDRulesAttribute();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //GET: api/LRD/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _lrdServices.Get(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //POST: api/LRD
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LrddatasDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _lrdServices.CreateLRD(data);
            if (result !=null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        // PUT: api/LRD/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] LrddatasDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != data.LrddataId)
            {
                return BadRequest();
            }
            var result = await _lrdServices.UpdateLRD(id, data);
            if (result == true)
            {
                return Ok(Properties.Resources.ResourceManager.GetString("SuccUpdateMessage"));
            }
            return BadRequest();
        }

        // DELETE: api/LRD/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            LrddatasDto result = await _lrdServices.DeleteLRD(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}