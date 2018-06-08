using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using alltrades_bot.Core;
using alltrades_bot.Core.Entities.Twitter;

namespace alltrades_bot.Business.Commands
{
    public class HandleMentionsCommand : BaseAsyncCommand<bool>
    {
        private readonly List<Tweet> _tweets;

        public HandleMentionsCommand(
            List<Tweet> tweets)
            {
                _tweets = tweets;        
            }

        protected override async Task<bool> ImplementExecute()
        {
            foreach(var tweet in _tweets)
            {
                Console.WriteLine(tweet.text);
            }

            return true;
        }
    }
}