using Microsoft.AspNetCore.Mvc;

namespace Aguila.Controllers
{
    /// <summary>
    /// Asset Bundle API for downloading the assetsbundle
    /// </summary>
    [Produces("application/json")]
    [Route("api/AssetBundle")]
    public class AssetBundleController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var response = File(id, "application/octet-stream");
            return response;
        }
    }
}