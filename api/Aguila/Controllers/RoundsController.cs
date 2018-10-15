using Aguila.Models;
using Aguila.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aguila.Controllers
{
    /// <summary>
    /// This is an User Rounds API which contains GET, PUT, POST DELETE methods
    /// </summary>
    [Produces("application/json")]
    [Route("api/Rounds")]
    [Authorize(Roles = "Administrator, User")]
    public class RoundsController : Controller
    {
        private readonly IRoundService _roundServices = null;
        public RoundsController(IRoundService roundServices)
        {
            this._roundServices = roundServices;
        }

        // GET: api/Rounds/GetAllMyRounds/12345
        [Route("GetAllMyRounds/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetRounds([FromRoute] string userId)
        {
            var result = await _roundServices.GetRounds(userId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/Rounds/GetMyRound/5
        [Route("GetMyRound/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetRoundsById([FromRoute] int id)
        {
            var result = await _roundServices.GetRoundsById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        // GET: api/Rounds/ScoreRound/5
        [Route("ScoreRound/{id}")]
        [HttpGet]
        public async Task<IActionResult> ScoreRound([FromRoute] int id)
        {
            var result = await _roundServices.ScoreRound(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/Rounds/ScoreMyRounds/5
        [Route("ScoreMyRounds/{id}/{situations1}/{situations2}/{situations3}/{situations4}")]
        [HttpGet]
        public async Task<IActionResult> ScoreMyRounds([FromRoute] string id, [FromRoute] int situations1, [FromRoute] int situations2, [FromRoute] int situations3, [FromRoute] int situations4)
        {
            var result = await _roundServices.ScoreMyRounds(id, situations1, situations2, situations3, situations4);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }


        //POST: api/Rounds
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRoundCompletedsDto data)
        {
            SuccessDto res = new SuccessDto();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            res.Success = await _roundServices.CreateRounds(data);
            if (res.Success == true)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

    }
}