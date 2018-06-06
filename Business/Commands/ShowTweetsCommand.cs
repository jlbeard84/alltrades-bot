using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using alltrades_bot.Core;
using alltrades_bot.Core.Entities;
using alltrades_bot.Core.Entities.Twitter;
using alltrades_bot.DataAccess;

namespace alltrades_bot.Business.Commands
{
    public class ShowTweetsCommand : BaseAsyncCommand<IList<Tweet>>
    {
        private readonly ITwitterRepository _twitterRepository;

        public ShowTweetsCommand(ITwitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
        }

        protected override async Task<IList<Tweet>> ImplementExecute()
        {
            var tweets = await _twitterRepository.GetTweets();

            return tweets;
        }
    }
}