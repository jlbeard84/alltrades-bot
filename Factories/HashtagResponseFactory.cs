using System.Collections.Generic;
using System.Linq;
using alltrades_bot.Business.Responses;
using alltrades_bot.Core;
using alltrades_bot.Core.Entities;
using alltrades_bot.Core.Entities.Twitter;

namespace alltrades_bot.Factories
{
    public class HashtagResponseFactory : IHashtagResponseFactory
    {
        public List<ITwitterResponseMessage> Generate(Tweet tweet)
        {
            var responseCommands = new List<BaseTwitterResponse>();

            var responses = new List<ITwitterResponseMessage>();

            var hashtags = new List<string>();

            if (!tweet.truncated.HasValue || !tweet.truncated.Value)
            {
                hashtags.AddRange(tweet
                    ?.entities
                    ?.hashtags
                    ?.Select(ht => ht.text));
            }
            else 
            {
                hashtags.AddRange(tweet
                    ?.extended_tweet
                    ?.entities
                    ?.hashtags
                    ?.Select(ht => ht.text));
            }

            foreach(var hashtag in hashtags)
            {
                switch(hashtag)
                {
                    case "roll":
                        responseCommands.Add(new RollResponse(tweet));
                        break;

                    default: 
                        responseCommands.Add(new UnknownResponse(tweet));
                        break;
                }
            }

            if (!responseCommands.Any())
            {
                responseCommands.Add(new DefaultResponse(tweet));
            }

            foreach(var response in responseCommands)
            {
                responses.Add(response.Respond());
            }

            return responses;
        }
    }
}