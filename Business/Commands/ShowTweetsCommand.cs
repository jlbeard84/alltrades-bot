using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using alltrades_bot.Core;
using alltrades_bot.Core.Entities;
using alltrades_bot.DataAccess;

namespace alltrades_bot.Business.Commands
{
    public class ShowTweetsCommand : BaseAsyncCommand<IList<string>>
    {
        private readonly ITwitterRepository _twitterRepository;

        public ShowTweetsCommand(ITwitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
        }

        protected override async Task<IList<string>> ImplementExecute()
        {
            var authToken = await _twitterRepository.GetAccessToken();

            var tweetTexts = new List<string>();

            return tweetTexts;
        }
    }
}