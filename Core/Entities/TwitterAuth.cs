namespace alltrades_bot.Core.Entities {
    public class TwitterAuth : ITwitterAuth {
        public string token_type { get; set; }

        public string access_token { get; set; }
    }
}