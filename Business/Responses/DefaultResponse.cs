using alltrades_bot.Core;
using alltrades_bot.Core.Entities;
using alltrades_bot.Core.Entities.Twitter;
using alltrades_bot.Core.Options;

namespace alltrades_bot.Business.Responses
{
    public class DefaultResponse : BaseTwitterResponse
    {
        private const string Message = "Hey @{0}, thanks for the message! Check here for what I can do: {1}";

        public DefaultResponse(
            Tweet tweet,
            ITwitterOptions twitterOptions)
            : base(
                tweet,
                twitterOptions) 
        { }

        protected override ITwitterResponseMessage ImplementRespond()
        {
            var responseText = string.Format(
                Message,
                Tweet.user.screen_name,
                TwitterOptions.CommandLink);

            var responseMessage = new TwitterResponseMessage(
                responseText);

            return responseMessage;
        }
    }
}