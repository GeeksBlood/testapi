using Aguila.Models;
using Aguila.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aguila.Controllers
{
    /// <summary>
    /// This is a PictureBooks API which contains GET, PUT, POST DELETE methods
    /// </summary>
    [Produces("application/json")]
    [Route("api/PictureBooks")]
    [Authorize(Roles = "Administrator, User")]
    public class PictureBooksController : Controller
    {
        private readonly IPictureBookService _pictureBookServices = null;

        public PictureBooksController(IPictureBookService pictureBookServices)
        {
            this._pictureBookServices = pictureBookServices;
        }

        // GET: api/PictureBooks
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _pictureBookServices.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/PictureBooksAdmin
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [Route("PictureBooksAdmin")]
        public async Task<IActionResult> PictureBooksAdmin()
        {
            var result = await _pictureBookServices.GetAllByAdmin();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/PictureBooks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _pictureBookServices.GetByID(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //POST: api/PictureBooks
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PictureBooksDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _pictureBookServices.Insert(data);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        // PUT: api/PictureBooks/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PictureBooksDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != data.Id)
            {
                return BadRequest();
            }
            var result = await _pictureBookServices.Update(id, data);
            if (result == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE: api/PictureBooks/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool result = await _pictureBookServices.Delete(id);
            if (result == true)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}