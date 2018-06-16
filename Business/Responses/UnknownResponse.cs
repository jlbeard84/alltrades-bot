using alltrades_bot.Core;
using alltrades_bot.Core.Entities;
using alltrades_bot.Core.Entities.Twitter;

namespace alltrades_bot.Business.Responses
{
    public class UnknownResponse : BaseTwitterResponse
    {
        private const string Message = "Hey @{0}, thanks for the message! Unfortunately I can't respond to that one. Please see my website for more details on what I can do.";

        public UnknownResponse(Tweet tweet)
            : base(tweet) { }

        protected override ITwitterResponseMessage ImplementRespond()
        {
            var responseMessage = new TwitterResponseMessage(string.Format(Message, Tweet.user.screen_name));

            return responseMessage;
        }
    }
}