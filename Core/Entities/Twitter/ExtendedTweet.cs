namespace alltrades_bot.Core.Entities.Twitter {
    public class ExtendedTweet {
        public string full_text { get; set; }

        public TweetEntities entities { get; set; }
    }
}