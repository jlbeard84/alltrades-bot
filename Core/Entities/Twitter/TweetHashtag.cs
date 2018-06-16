using System.Collections.Generic;

namespace alltrades_bot.Core.Entities.Twitter {
    public class TweetHashtag {
        public string text { get; set; }

        public List<int> indices { get; set; } = new List<int> ();
    }
}