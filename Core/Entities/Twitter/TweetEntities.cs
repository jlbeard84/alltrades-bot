using System.Collections.Generic;

namespace alltrades_bot.Core.Entities.Twitter {
    public class TweetEntities {
        public List<TweetHashtag> hashtags { get; set; } = new List<TweetHashtag> ();
    }
}