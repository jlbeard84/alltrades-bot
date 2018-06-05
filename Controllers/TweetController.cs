using System.Collections.Generic;
using System.Threading.Tasks;
using alltrades_bot.Business.Commands;
using Microsoft.AspNetCore.Mvc;

namespace alltrades_bot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController
    {
        [HttpGet]
        public async Task<ActionResult<IList<string>>> Get()
        {
            var command = new ShowTweetsCommand();
            var returnObject = await command.Execute();
            return new OkObjectResult(returnObject);
        }
    }
}