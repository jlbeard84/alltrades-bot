using System.Collections.Generic;
using System.Linq;
using alltrades_bot.Business.Responses;
using alltrades_bot.Core;
using alltrades_bot.Core.Entities;
using alltrades_bot.Core.Entities.Twitter;
using alltrades_bot.Core.Options;
using alltrades_bot.DataAccess;
using Microsoft.Extensions.Options;

namespace alltrades_bot.Factories
{
    public class HashtagResponseFactory : IHashtagResponseFactory
    {
        private readonly ITwitterOptions _options;

        public HashtagResponseFactory(
            IOptions<TwitterOptions> options)
        {
            _options = options.Value;
        }

        public List<ITwitterResponseMessage> Generate(Tweet tweet)
        {
            var responseCommands = new List<BaseTwitterResponse>();

            var responses = new List<ITwitterResponseMessage>();

            var hashtags = new List<string>();

            if (!tweet.truncated.HasValue || !tweet.truncated.Value)
            {
                var tags = tweet
                    ?.entities
                    ?.hashtags
                    ?.Select(ht => ht.text);

                hashtags.AddRange(tags);
            }
            else 
            {
                var tags = tweet
                    ?.extended_tweet
                    ?.entities
                    ?.hashtags
                    ?.Select(ht => ht.text);

                hashtags.AddRange(tags);
            }

            hashtags = hashtags
                .Distinct()
                .ToList();

            foreach(var hashtag in hashtags)
            {
                switch(hashtag)
                {
                    case "roll":
                        responseCommands.Add(new RollResponse(
                            tweet,
                            _options));
                        break;

                    default: 
                        responseCommands.Add(new UnknownResponse(
                            tweet,
                            _options));
                        break;
                }
            }

            if (!responseCommands.Any())
            {
                responseCommands.Add(new DefaultResponse(
                    tweet,
                    _options));
            }

            foreach(var response in responseCommands)
            {
                responses.Add(response.Respond());
            }

            return responses;
        }
    }
}