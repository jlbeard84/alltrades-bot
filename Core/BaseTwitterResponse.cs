using alltrades_bot.Core.Entities;
using alltrades_bot.Core.Entities.Twitter;
using alltrades_bot.Core.Options;
using alltrades_bot.DataAccess;

namespace alltrades_bot.Core
{
    public abstract class BaseTwitterResponse
    {
        protected readonly Tweet Tweet;

        protected readonly ITwitterOptions TwitterOptions;

        public BaseTwitterResponse(
            Tweet tweet,
            ITwitterOptions twitterOptions)
        {
            Tweet = tweet;
            TwitterOptions = twitterOptions;
        }

        protected abstract ITwitterResponseMessage ImplementRespond();

        public virtual ITwitterResponseMessage Respond()
        {
            var response = ImplementRespond();
            response.ResponseID = Tweet.id_str;

            return response;
        }
    }
}