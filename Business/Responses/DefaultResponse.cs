using alltrades_bot.Core;
using alltrades_bot.Core.Entities;
using alltrades_bot.Core.Entities.Twitter;

namespace alltrades_bot.Business.Responses
{
    public class DefaultResponse : BaseTwitterResponse
    {
        private const string Message = "Hey @{0}, thanks for the message! Right now I can't do much, but stay tuned for updates!";

        public DefaultResponse(Tweet tweet)
            : base(tweet) { }

        protected override ITwitterResponseMessage ImplementRespond()
        {
            var responseMessage = new TwitterResponseMessage(string.Format(Message, Tweet.user.screen_name));

            return responseMessage;
        }
    }
}