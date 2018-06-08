namespace alltrades_bot.Core.Options
{
    public class TwitterOptions : ITwitterOptions
    {
        public string ApiBase { get; set; }

        public string OAuthEndpoint { get; set; }

        public string OAuthGrantType { get; set; }

        public string ConsumerKey { get; set; }
        
        public string ConsumerSecret { get; set; }

        public string UserId { get; set; }

        public string UserTimelineEndpoint { get; set; }

        public string SearchTweetsEndpoint { get; set; }
    }
}