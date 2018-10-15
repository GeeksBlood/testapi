using Aguila.Models;
using Aguila.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aguila.Controllers
{
    /// <summary>
    /// This is a Situations API which contains GET, PUT, POST DELETE methods
    /// </summary>
    [Produces("application/json")]
    [Route("api/Situations")]
    [Authorize(Roles = "Administrator, User")]
    public class SituationsController : Controller
    {
        private readonly ISituationService _situationService;
        public SituationsController(ISituationService situationService)
        {
            _situationService = situationService;
        }

        // GET: api/Situations
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _situationService.GetSituation();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/Situations/SituationByHole
        [Route("SituationByHole")]
        [HttpGet]
        public async Task<IActionResult> SituationByHole()
        {
            var result = await _situationService.GetSituationByHole();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/Situations/AllSituation
        [Route("AllSituation")]
        [HttpGet]
        public async Task<IActionResult> AllSituation()
        {
            var result = await _situationService.GetAllSituation();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/Situations/SituationIsFirstHole
        [Route("SituationIsFirstHole")]
        [HttpGet]
        public async Task<IActionResult> SituationIsFirstHole()
        {
            var result = await _situationService.GetSituationIsFirstHole();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        // GET: api/Situations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _situationService.GetSituationByID(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/Situations/GetSituationsByCHA/A/C/H
        [Route("GetSituationsByCHA/{A}/{C}/{H}")]
        [HttpGet]
        public async Task<IActionResult> GetSituationsByCHA([FromRoute] int[] A, [FromRoute] int C, [FromRoute] int[] H)
        {
            var result = await _situationService.GetSituationsByCHA(C, H, A);
            return Ok(result);

        }

        // GET: api/Situations/SituationCategories
        [Route("SituationCategories")]
        [HttpGet]
        public async Task<IActionResult> GetSituationCategories()
        {
            var result = await _situationService.GetSituationCategories();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //POST: api/Situations
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewSituationViewDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _situationService.CreateSituation(data);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }


        // PUT: api/Situations/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody]  EditSituationViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != data.Id)
            {
                return BadRequest();
            }
            var result = await _situationService.UpdateSituation(id, data);
            if (result !=null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        //PUT: api/Situations/PublishSituation/5
        [Authorize(Roles = "Administrator")]
        [Route("PublishSituation/{id}")]
        [HttpPut]
        public async Task<IActionResult> PublishSituation([FromRoute] int id)
        {
            SituationsDto result = await _situationService.PublishSituation(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // PUT: api/Situations/UnPublishSituation/5
        [Authorize(Roles = "Administrator")]
        [Route("UnPublishSituation/{id}")]
        [HttpPut]
        public async Task<IActionResult> UnPublishSituation([FromRoute] int id)
        {
            SituationsDto result = await _situationService.UnPublishSituation(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // DELETE: api/Situations/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool result = await _situationService.DeleteSituation(id);
            if (result == true)
            {
                return Ok(Properties.Resources.ResourceManager.GetString("SuccDeleteMessage"));
            }
            return NotFound();
        }

        // GET: api/Situations/GetSituationsByNextHoleId
        [Route("GetSituationsByNextHoleId/{id}")]       
        [HttpGet]
        public async Task<IActionResult> GetSituationsByNextHoleId([FromRoute] int id)
        {
            var result = await _situationService.GetSituationsByNextHoleId(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}