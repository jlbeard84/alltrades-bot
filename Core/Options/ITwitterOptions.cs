namespace alltrades_bot.Core.Options
{
    public interface ITwitterOptions
    {
        string ApiBase { get; set; }

        string OAuthEndpoint { get; set; }

        string OAuthGrantType  {get; set; }

        string ConsumerKey { get; set; }

        string ConsumerSecret { get; set; }

        string UserId { get; set; }

        string UserTimelineEndpoint { get; set; }

        string SearchTweetsEndpoint { get; set; }

        string MaxIdFileName { get; set; }

        string UserAccessToken { get; set; }

        string UserAccessSecret { get; set; }

        string SendTweetEndpoint { get; set; }
    }
}