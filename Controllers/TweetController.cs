using System.Collections.Generic;
using System.Threading.Tasks;
using alltrades_bot.Business.Commands;
using alltrades_bot.DataAccess;
using alltrades_bot.Factories;
using Microsoft.AspNetCore.Mvc;

namespace alltrades_bot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController
    {
        private readonly ITwitterRepository _twitterRepository;
        private readonly IHashtagResponseFactory _hashtagResponseFactory;

        public TweetController(
            ITwitterRepository twitterRepository,
            IHashtagResponseFactory hashtagResponseFactory) 
        {
            _twitterRepository = twitterRepository;
            _hashtagResponseFactory = hashtagResponseFactory;
        }

        [HttpGet]
        public async Task<ActionResult<IList<string>>> Get()
        {
            var command = new ShowTweetsCommand(
                _twitterRepository);

            var returnObject = await command.Execute();
            
            return new OkObjectResult(returnObject);
        }

        [HttpGet("Watch")]
        public async Task<ActionResult<bool>> StartMentionWatch()
        {
            var command = new FireAndForgetMentionsCommand(
                _twitterRepository,
                _hashtagResponseFactory);

            var returnObject = await command.Execute();
            
            return new OkObjectResult(returnObject);
        }
    }
}