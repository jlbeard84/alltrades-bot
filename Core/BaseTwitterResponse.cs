using alltrades_bot.Core.Entities;
using alltrades_bot.Core.Entities.Twitter;

namespace alltrades_bot.Core
{
    public abstract class BaseTwitterResponse
    {
        protected readonly Tweet Tweet;

        public BaseTwitterResponse(Tweet tweet)
        {
            Tweet = tweet;
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