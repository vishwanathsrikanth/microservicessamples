using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleAPIApp
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<HttpResponseMessage> Get(int id)
        {
            var client = new HttpClient();
            var uri = "https://apim-workbench.azure-api.net/FunctionApp120190531112311/Function1?name=sriks";
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
            httpRequestMessage.Headers.Add("Ocp-Apim-Subscription-Key", "6d7140fb933644cb8af003fec6f4acbf");
            HttpResponseMessage response = await client.SendAsync(httpRequestMessage);
            return response;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
