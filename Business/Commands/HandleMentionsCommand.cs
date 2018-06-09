using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using alltrades_bot.Core;
using alltrades_bot.Core.Entities.Twitter;
using alltrades_bot.DataAccess;

namespace alltrades_bot.Business.Commands
{
    public class HandleMentionsCommand : BaseAsyncCommand<bool>
    {
        private readonly ITwitterRepository _twitterRepository;

        private readonly List<Tweet> _tweets;

        public HandleMentionsCommand(
            List<Tweet> tweets,
            ITwitterRepository twitterRepository)
            {
                _tweets = tweets;
                _twitterRepository = twitterRepository;
            }

        protected override async Task<bool> ImplementExecute()
        {
            foreach(var tweet in _tweets)
            {
                try
                {
                    var message = $"Hey @{tweet.user.screen_name}, thanks for the message! Right now I can't do much, but stay tuned for updates!";

                    await _twitterRepository.SendTweet(
                        message,
                        tweet.id_str);
                }
                catch(Exception)
                {
                    //TODO: queue this for later
                    Console.WriteLine($"Failed to write response to {tweet.id_str}");
                }
            }

            return true;
        }
    }
}