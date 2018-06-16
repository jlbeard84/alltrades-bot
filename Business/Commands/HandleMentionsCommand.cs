using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using alltrades_bot.Core;
using alltrades_bot.Core.Entities.Twitter;
using alltrades_bot.DataAccess;
using alltrades_bot.Factories;

namespace alltrades_bot.Business.Commands
{
    public class HandleMentionsCommand : BaseAsyncCommand<bool>
    {
        private readonly ITwitterRepository _twitterRepository;

        private readonly IHashtagResponseFactory _hashtagResponseFactory;

        private readonly List<Tweet> _tweets;

        public HandleMentionsCommand(
            List<Tweet> tweets,
            ITwitterRepository twitterRepository,
            IHashtagResponseFactory hashtagResponseFactory)
            {
                _tweets = tweets;
                _twitterRepository = twitterRepository;
                _hashtagResponseFactory = hashtagResponseFactory;
            }

        protected override async Task<bool> ImplementExecute()
        {
            foreach(var tweet in _tweets)
            {
                var responseMessages = _hashtagResponseFactory
                        .Generate(tweet);

                foreach(var responseMessage in responseMessages)
                {
                    try
                    {
                        await _twitterRepository.SendTweet(
                            responseMessage);
                    }
                    catch(Exception)
                    {
                        //TODO: queue this for later
                        Console.WriteLine($"Failed to write response to {tweet.id_str}");
                    }
                }
            }

            return true;
        }
    }
}