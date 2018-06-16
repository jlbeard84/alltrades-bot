namespace alltrades_bot.Core.Entities.Twitter {
    public class Tweet {
        public string created_at { get; set; }

        public long? id { get; set; }

        public string id_str { get; set; }

        public string text { get; set; }

        public bool? truncated { get; set; }

        public string source { get; set; }

        public long? in_reply_to_status_id { get; set; }

        public User user { get; set; }

        public string in_reply_to_status_id_str { get; set; }

        public long? in_reply_to_user_id { get; set; }

        public string in_reply_to_user_id_str { get; set; }

        public string in_reply_to_screen_name { get; set; }

        public bool? is_quote_status { get; set; }

        public long? retweet_count { get; set; }

        public long? favorite_count { get; set; }

        public bool? favorited { get; set; }

        public bool? retweeted { get; set; }

        public bool? possibly_sensitive { get; set; }
        
        public string lang { get; set; }

        public TweetEntities entities { get; set; }

        public ExtendedTweet extended_tweet { get; set; }
    }
}