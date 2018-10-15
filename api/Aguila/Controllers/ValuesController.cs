using Aguila.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Aguila.Controllers
{
    /// <summary>
    /// This is a Values API
    /// </summary>
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly AguilaContext answerServices = null;

        public ValuesController(AguilaContext _answerServices)
        {
            answerServices = _answerServices;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //var data = answerServices.Answers.ToList();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
