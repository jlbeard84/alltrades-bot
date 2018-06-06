namespace alltrades_bot.Core.Options
{
    public interface ITwitterOptions
    {
        string ApiBase { get; set; }

        string OAuthEndpoint { get; set; }

        string OAuthGrantType  {get; set; }

        string ConsumerKey { get; set; }

        string ConsumerSecret { get; set; }
    }
}