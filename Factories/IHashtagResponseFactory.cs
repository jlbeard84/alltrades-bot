using System.Collections.Generic;
using alltrades_bot.Core.Entities;
using alltrades_bot.Core.Entities.Twitter;

namespace alltrades_bot.Factories
{
    public interface IHashtagResponseFactory
    {
        List<ITwitterResponseMessage> Generate(Tweet tweet);
    }
}