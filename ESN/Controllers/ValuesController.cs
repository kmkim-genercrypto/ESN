using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComLib;

//사용안함
namespace ESN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAccountBiz _esnBiz;

        public ValuesController(IAccountBiz esnBiz)
        {
            _esnBiz = esnBiz;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var str = await _esnBiz.GetBalanceAsync("0x8567bfdd8a06134d798f053f914598087ed57b76", "latest");

            return new string[] { "BlockHeight", str.ToString() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var str = await _esnBiz.GetBalanceAsync("0x8567bfdd8a06134d798f053f914598087ed57b76", "latest");

            return str;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
